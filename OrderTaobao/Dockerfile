#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrderTaobao/BaseSource.BackendAPI.csproj", "OrderTaobao/"]
COPY ["BaseSource.Builder/BaseSource.Builder.csproj", "BaseSource.Builder/"]
COPY ["Basesource.Constants/Basesource.Constants.csproj", "Basesource.Constants/"]
COPY ["BaseSource.Dto/BaseSource.Dto.csproj", "BaseSource.Dto/"]
COPY ["BaseSource.Model/BaseSource.Model.csproj", "BaseSource.Model/"]
COPY ["BaseSource/BaseSource.csproj", "BaseSource/"]
COPY ["BaseSource.Infrastructure/BaseSource.Infrastructure.csproj", "BaseSource.Infrastructure/"]
COPY ["OrderTaobao.Service/BaseSource.BackendAPI.Services.csproj", "OrderTaobao.Service/"]
RUN dotnet restore "OrderTaobao/BaseSource.BackendAPI.csproj"
COPY . .
WORKDIR "/src/OrderTaobao"
RUN dotnet build "BaseSource.BackendAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseSource.BackendAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseSource.BackendAPI.dll"]