#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Eparafia.Parish.API/Eparafia.Parish.API.csproj", "Eparafia.Parish.API/"]
COPY ["Eparafia.Parish.Application/Eparafia.Parish.Application.csproj", "Eparafia.Parish.Application/"]
COPY ["Eparafia.Parish.Domain/Eparafia.Parish.Domain.csproj", "Eparafia.Parish.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Eparafia.Parish.Infrastructure/Eparafia.Parish.Infrastructure.csproj", "Eparafia.Parish.Infrastructure/"]
RUN dotnet restore "Eparafia.Parish.API/Eparafia.Parish.API.csproj"
COPY . .
WORKDIR "/src/Eparafia.Parish.API"
RUN dotnet build "Eparafia.Parish.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eparafia.Parish.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eparafia.Parish.API.dll"]