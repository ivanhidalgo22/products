# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
#RUN apt-get update --yes && apt-get install --yes procps && rm -rf /var/lib/apt/lists/*
#RUN echo 'root:123' | chpasswd
#RUN echo "PasswordAuthentication yes" >> /etc/ssh/sshd_config
#RUN echo "PermitRootLogin yes" >> /etc/ssh/sshd_config
#RUN update-rc.d ssh enable

WORKDIR /vsdbg
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        unzip \
    && rm -rf /var/lib/apt/lists/* \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg 

WORKDIR /app
COPY --from=build /app .
EXPOSE 80
EXPOSE 443
EXPOSE 5001
ENV ASPNETCORE_ENVIRONMENT='Development'
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "Swo.Chaas.Products.API.dll"]
#ENTRYPOINT ["bash", "-c", "/etc/init.d/ssh start && dotnet helloworld.dll"]