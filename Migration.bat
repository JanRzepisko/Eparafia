@echo off

set arg1=%1

cd Eparafia.Parish.API
dotnet ef migrations add %arg1% --project ..\Eparafia.Parish.Infrastructure\Eparafia.Parish.Infrastructure.csproj
dotnet ef database update  -- --environment  Development

cd ..
cd Eparafia.Identity.API

dotnet ef migrations add %arg1% --project ..\Eparafia.Identity.Infrastructure\Eparafia.Identity.Infrastructure.csproj
dotnet ef database update  -- --environment  Development

cd ..
cd Eparafia.Administration.API

dotnet ef migrations add %arg1% --project ..\Eparafia.Administration.Infrastructure\Eparafia.Administration.Infrastructure.csproj
dotnet ef database update -- --environment Development