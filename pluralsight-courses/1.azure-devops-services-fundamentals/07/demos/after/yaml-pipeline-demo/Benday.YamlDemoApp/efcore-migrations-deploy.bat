@ECHO OFF

set base=%cd%
cd src\Benday.YamlDemoApp.Api

IF "%1" == "" (
dotnet ef database update
) ELSE (
dotnet ef database update %1
)

cd %base%