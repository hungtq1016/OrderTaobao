#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/AddressService/AddressService.csproj", "src/services/AddressService/"]
COPY ["src/libs/Core/Core.csproj", "src/libs/Core/"]
COPY ["src/libs/Infrastructure.EFCore/Infrastructure.EFCore.csproj", "src/libs/Infrastructure.EFCore/"]
COPY ["src/libs/Infrastructure.Main/Infrastructure.Main.csproj", "src/libs/Infrastructure.Main/"]
RUN dotnet restore "./src/services/AddressService/./AddressService.csproj"
COPY . .
WORKDIR "/src/src/services/AddressService"
RUN dotnet build "./AddressService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AddressService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressService.dll"]