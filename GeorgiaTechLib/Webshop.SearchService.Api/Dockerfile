# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN ls . # debugging help
COPY ["Webshop.SearchService.Api/Webshop.SearchService.Api.csproj", "Webshop.SearchService.Api/"]
COPY ["Webshop.Tools.Messaging/Webshop.Tools.Messaging.csproj", "Webshop.Tools.Messaging/"]
RUN dotnet restore "./Webshop.SearchService.Api/Webshop.SearchService.Api.csproj"
COPY . .
WORKDIR "/src/Webshop.SearchService.Api"
RUN dotnet build "./Webshop.SearchService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Webshop.SearchService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webshop.SearchService.Api.dll"]