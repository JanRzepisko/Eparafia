#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Eparafia.Bible.API/Eparafia.Bible.API.csproj", "Eparafia.Bible.API/"]
COPY ["Eparafia.Bible.Application/Eparafia.Bible.Application.csproj", "Eparafia.Bible.Application/"]
COPY ["Eparafia.Bible.Domain/Eparafia.Bible.Domain.csproj", "Eparafia.Bible.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Eparafia.Bible.Infrastructure/Eparafia.Bible.Infrastructure.csproj", "Eparafia.Bible.Infrastructure/"]
RUN dotnet restore "Eparafia.Bible.API/Eparafia.Bible.API.csproj"
COPY . .
WORKDIR "/src/Eparafia.Bible.API"
RUN dotnet build "Eparafia.Bible.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eparafia.Bible.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eparafia.Bible.API.dll"]