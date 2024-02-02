#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/OAuth2Service/OAuth2Service.csproj", "src/services/OAuth2Service/"]
COPY ["src/libs/Core/Core.csproj", "src/libs/Core/"]
COPY ["src/libs/Infrastructure.EFCore/Infrastructure.EFCore.csproj", "src/libs/Infrastructure.EFCore/"]
COPY ["src/libs/Infrastructure.Main/Infrastructure.Main.csproj", "src/libs/Infrastructure.Main/"]
RUN dotnet restore "src/services/OAuth2Service/OAuth2Service.csproj"
COPY . .
WORKDIR "/src/src/services/OAuth2Service"
RUN dotnet build "OAuth2Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OAuth2Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OAuth2Service.dll"]