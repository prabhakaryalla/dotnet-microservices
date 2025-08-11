#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shopping.Api.Host/Shopping.Api.Host.csproj", "Shopping.Api.Host/"]
COPY ["Shopping.Api.Controllers/Shopping.Api.Controllers.csproj", "Shopping.Api.Controllers/"]
COPY ["Shopping.Api.Contracts/Shopping.Api.Contracts.csproj", "Shopping.Api.Contracts/"]
COPY ["Shopping.Api.IOC/Shopping.Api.IOC.csproj", "Shopping.Api.IOC/"]
COPY ["Shopping.Api.Managers/Shopping.Api.Managers.csproj", "Shopping.Api.Managers/"]
COPY ["Shopping.Api.OpenApi/Shopping.Api.OpenApi.csproj", "Shopping.Api.OpenApi/"]
COPY ["Shopping.Api.Services/Shopping.Api.Services.csproj", "Shopping.Api.Services/"]
RUN dotnet restore "./Shopping.Api.Host/Shopping.Api.Host.csproj"
COPY . .
WORKDIR "/src/Shopping.Api.Host"
RUN dotnet build "./Shopping.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Shopping.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Api.Host.dll"]