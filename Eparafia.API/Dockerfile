#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Eparafia.API/Eparafia.API.csproj", "Eparafia.API/"]
COPY ["Eparafia.Application/Eparafia.Application.csproj", "Eparafia.Application/"]
COPY ["Eparafia.Domain/Eparafia.Domain.csproj", "Eparafia.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Eparafia.Infrastructure/Eparafia.Infrastructure.csproj", "Eparafia.Infrastructure/"]
RUN dotnet restore "Eparafia.API/Eparafia.API.csproj"
COPY . .
WORKDIR "/src/Eparafia.API"
RUN dotnet build "Eparafia.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eparafia.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eparafia.API.dll"]