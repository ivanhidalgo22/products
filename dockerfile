# Stage 1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish -c Debug -o /app 
# Stage 2
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:7.0
#RUN apt-get update --yes && apt-get install --yes procps && rm -rf /var/lib/apt/lists/*

#RUN mkdir /build
#WORKDIR /build
#COPY . .

WORKDIR /app
COPY --from=build-env /app .
#COPY --from=build /build /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001
ENV ASPNETCORE_URLS http://*:5001
#ENV MYSQL_SERVER=""
#ENV MYSQL_USER=""
#ENV MYSQL_PASSWORD=""
ENTRYPOINT ["dotnet", "Sample.Marketplace.Products.API.dll"]