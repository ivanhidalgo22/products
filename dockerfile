# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build

COPY . .
RUN dotnet restore
RUN dotnet publish -c Debug -o /app 
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
RUN apt-get update --yes && apt-get install --yes procps && rm -rf /var/lib/apt/lists/*

RUN mkdir /build
#WORKDIR /build
#COPY . .

WORKDIR /app
COPY --from=build /app .
COPY --from=build /build /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001
ENV ASPNETCORE_ENVIRONMENT='Development'
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "Swo.Chaas.Products.API.dll"]