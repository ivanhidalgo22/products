$content=Get-Content ".\deployment\manifest.yaml"
if ([String]::IsNullOrEmpty($content)) {
    helm template sd-gateway-configuration .\..\helm-charts\sd-gateway-configuration -f .\..\helm-charts\imageVersions.yaml -n scorpion-skaffold > deployment\manifest.yaml
}