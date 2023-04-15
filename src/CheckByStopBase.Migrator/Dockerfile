#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/CheckByStopBase.Migrator/CheckByStopBase.Migrator.csproj", "src/CheckByStopBase.Migrator/"]
COPY ["src/CheckByStopBase.CompanyStopBase.DAL/CheckByStopBase.CompanyStopBase.DAL.csproj", "src/CheckByStopBase.CompanyStopBase.DAL/"]
COPY ["src/CheckByStopBase.CompanyStopBase.Domain/CheckByStopBase.CompanyStopBase.Domain.csproj", "src/CheckByStopBase.CompanyStopBase.Domain/"]
COPY ["src/CheckByStopBase.Domain/CheckByStopBase.Domain.csproj", "src/CheckByStopBase.Domain/"]
RUN dotnet restore "src/CheckByStopBase.Migrator/CheckByStopBase.Migrator.csproj"
COPY . .
WORKDIR "/src/src/CheckByStopBase.Migrator"
RUN dotnet build "CheckByStopBase.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CheckByStopBase.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheckByStopBase.Migrator.dll"]