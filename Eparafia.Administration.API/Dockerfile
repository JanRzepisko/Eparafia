#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Eparafia.Administration.API/Eparafia.Administration.API.csproj", "Eparafia.Administration.API/"]
COPY ["Eparafia.Administration.Application/Eparafia.Administration.Application.csproj", "Eparafia.Administration.Application/"]
COPY ["Eparafia.Administration.Domain/Eparafia.Administration.Domain.csproj", "Eparafia.Administration.Domain/"]
COPY ["Eparafia.Administration.Infrastructure/Eparafia.Administration.Infrastructure.csproj", "Eparafia.Administration.Infrastructure/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Eparafia.Administration.API/Eparafia.Administration.API.csproj"
COPY . .
WORKDIR "/src/Eparafia.Administration.API"
RUN dotnet build "Eparafia.Administration.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eparafia.Administration.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eparafia.Administration.API.dll"]