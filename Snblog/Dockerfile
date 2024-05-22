#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Snblog/Snblog.csproj", "Snblog/"]
COPY ["Snblog.Enties/Snblog.Enties.csproj", "Snblog.Enties/"]
COPY ["Snblog.IService/Snblog.IService.csproj", "Snblog.IService/"]
COPY ["Snblog.Util/Snblog.Util.csproj", "Snblog.Util/"]
COPY ["Snblog.Service/Snblog.Service.csproj", "Snblog.Service/"]
COPY ["Snblog.Cache/Snblog.Cache.csproj", "Snblog.Cache/"]
RUN dotnet restore "./Snblog/Snblog.csproj"
COPY . .
WORKDIR "/src/Snblog"
RUN dotnet build "./Snblog.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Snblog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snblog.dll"]