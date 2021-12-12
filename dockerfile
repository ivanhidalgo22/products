# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
EXPOSE 443
EXPOSE 5001
ENV ASPNETCORE_ENVIRONMENT='Development'
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "Swo.Chaas.Products.API.dll"]