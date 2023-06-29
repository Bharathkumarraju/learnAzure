@ECHO OFF

set startingPath=%cd%
set base=%cd%
set databasename=benday-yaml-demo-app

echo "***** RESTORING DOTNET TOOLS *****"
dotnet tool restore

echo "***** DROPPING DATABASE *****"
sqlcmd -Q "drop database if exists [%databasename%]" -d master

echo "***** CREATING DATABASE *****"
sqlcmd -Q "create database [%databasename%]" -d master

cd src\Benday.YamlDemoApp.Api
echo %cd%

echo "***** DEPLOYING EF CORE MIGRATIONS *****"
dotnet ef database update

cd %base%

echo "***** POPULATING DATABASE PERMISSIONS *****"
cd misc\database
call populate-sql-server-permissions.bat

cd %base%

cd %startingPath%
echo "***** UPDATING LOOKUP VALUES *****"
cd misc\database
call update-lookup-values.bat

echo "cd %startingPath%"
cd %startingPath%

echo "***** ADDITIONAL DEPLOYMENT STEPS *****"
if exist deploy-additional-steps.bat (
    ECHO "Found deploy-additional-steps.bat.  Calling it..."
    call deploy-additional-steps.bat
    cd %startingPath%
) else (
    ECHO "INFO: No deploy-additional-steps.bat file found.  FYI, if you want to specify more things to do on deployment, create this file."
)

echo "***** DONE *****"