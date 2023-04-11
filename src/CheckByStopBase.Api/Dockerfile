#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/CheckByStopBase.Api/CheckByStopBase.Api.csproj", "src/CheckByStopBase.Api/"]
COPY ["src/CheckByStopBase.BackgroundServices/CheckByStopBase.BackgroundServices.csproj", "src/CheckByStopBase.BackgroundServices/"]
COPY ["src/CheckByStopBase.RegistryParsers/CheckByStopBase.RegistryParsers.csproj", "src/CheckByStopBase.RegistryParsers/"]
COPY ["src/CheckByStopBase.CompanyStopBase.DAL/CheckByStopBase.CompanyStopBase.DAL.csproj", "src/CheckByStopBase.CompanyStopBase.DAL/"]
COPY ["src/CheckByStopBase.CompanyStopBase.Domain/CheckByStopBase.CompanyStopBase.Domain.csproj", "src/CheckByStopBase.CompanyStopBase.Domain/"]
COPY ["src/CheckByStopBase.Domain/CheckByStopBase.Domain.csproj", "src/CheckByStopBase.Domain/"]
COPY ["src/CheckByStopBase.ServiceLayer/CheckByStopBase.ServiceLayer.csproj", "src/CheckByStopBase.ServiceLayer/"]
RUN dotnet restore "src/CheckByStopBase.Api/CheckByStopBase.Api.csproj"
COPY . .
WORKDIR "/src/src/CheckByStopBase.Api"
RUN dotnet build "CheckByStopBase.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CheckByStopBase.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheckByStopBase.Api.dll"]