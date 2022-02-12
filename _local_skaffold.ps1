param(
    [Alias('p')]
    [string]$Profile="dev"
)

function Write-Debug {
    param(
        [Parameter(mandatory=$true)][string]$Message
    )
    Write-Host -ForegroundColor Yellow $Message
}

function Write-Info {
    param(
        [Parameter(mandatory=$true)][string]$Message
    )
    Write-Host -ForegroundColor Cyan $Message
}

function Write-Warning {
    param(
        [Parameter(mandatory=$true)][string]$Message
    )
    Write-Host -ForegroundColor Magenta $Message
}

function Validate-Networking {
    # Free resource on port 443
    $OwningProcess = $(Get-NetTCPConnection -LocalPort 443 -ErrorAction SilentlyContinue).OwningProcess
    if ($OwningProcess) {
        Write-Debug -Message "Stopping Process $OwningProcess"
        Stop-Process $OwningProcess
        if ($LastExitCode -eq 0){
            Write-Info -Message "Process $OwningProcess stopped"
        }else {
            Write-Warning -Message "Could not stop process $OwningProcess"
        }
    }
}
function Clean-Up {
    param(
        [string]$Image,
        [string]$JobName="CleanUp"
    )
    if ($Image) {
        Write-Debug -Message "Deleting image $Image"
        az acr repository delete --name simpledevopsacr --image $image --yes
        if ($LastExitCode -eq 0){
            Write-Info -Message "Image $Image deleted"
        }else {
            Write-Warning -Message "Image $Image not found"
        }
    }
    Write-Info -Message "Deleting Jobs"
    $jobs = $(Get-Job -Name $JobName -ErrorAction SilentlyContinue).Id
    $jobs | foreach {
        if ($_) {
            Write-Debug -Message "Deleting Job $_"
            Stop-Job -ErrorAction SilentlyContinue -Id $_
            Remove-Job -ErrorAction SilentlyContinue -Id $_
            Write-Info -Message "Job $_ deleted"
        }
    }
}

$image=""
$job_name="Skaffold-Job"
try {
    if (-Not (Test-Path -Path ".\deployment\")) {
        New-Item -Path .\deployment\ -ItemType "directory"
        New-Item -Path .\deployment\manifest.yaml -ItemType "file"
    }
    $username=$($env:USERNAME).ToLower()
    $env:IMAGE_TAG="$username$(git rev-parse HEAD)"
    $env:IMAGE = "sd-gateway-configuration:$env:IMAGE_TAG"
    Validate-Networking
    Write-Info -Message "CleanUp Skaffold"
    Clean-Up -JobName $job_name -Image $env:IMAGE
    $wd=$(pwd).Path
    $object = [ordered]@{
        Wd = $wd;
        Profile = $Profile
    }
    Write-Info "Using profile $($object.Profile)"
    Start-Job -Name $job_name -ScriptBlock {
        $obj = $input.Clone()
        Set-Location -Path $obj.Wd
        skaffold run -p $obj.Profile --cache-artifacts=false --no-prune=false
    } -InputObject $object
    $job_id = $(Get-Job -Name $job_name).Id
    Write-Info -Message "Restoring Job $job_id"
    Receive-Job -Id $job_id -Wait -WriteEvents -WriteJobInResults
} finally {
    Write-Info -Message "CleanUp Skaffold"
    Clean-Up -JobName $job_name
    Write-Debug -Message "Deleting folder deployment"
    Remove-Item -Force -LiteralPath ".\deployment\" -Recurse
}