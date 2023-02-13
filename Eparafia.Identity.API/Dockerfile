#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Eparafia.Identity.API/Eparafia.Identity.API.csproj", "Eparafia.Identity.API/"]
COPY ["Eparafia.Identity.Application/Eparafia.Identity.Application.csproj", "Eparafia.Identity.Application/"]
COPY ["Eparafia.Identity.Domain/Eparafia.Identity.Domain.csproj", "Eparafia.Identity.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Eparafia.Identity.Infrastructure/Eparafia.Identity.Infrastructure.csproj", "Eparafia.Identity.Infrastructure/"]
RUN dotnet restore "Eparafia.Identity.API/Eparafia.Identity.API.csproj"
COPY . .
WORKDIR "/src/Eparafia.Identity.API"
RUN dotnet build "Eparafia.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eparafia.Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eparafia.Identity.API.dll"]