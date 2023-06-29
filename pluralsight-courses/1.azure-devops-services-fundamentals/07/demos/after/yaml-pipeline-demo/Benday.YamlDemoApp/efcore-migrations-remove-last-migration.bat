set base=%cd%
cd src\Benday.YamlDemoApp.Api

dotnet ef migrations remove

cd %base%