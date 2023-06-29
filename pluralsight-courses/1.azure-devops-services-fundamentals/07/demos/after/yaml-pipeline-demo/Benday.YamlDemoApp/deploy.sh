#!/bin/bash

base="$PWD"

dropDatabaseDefaultValue="0";
dropDatabase=$dropDatabaseDefaultValue;

deployEfCoreMigrationsDefaultValue="1";
deployEfCoreMigrations=$deployEfCoreMigrationsDefaultValue;

runAdditionalStepsDefaultValue="1";
runAdditionalSteps=$runAdditionalStepsDefaultValue;

servername="127.0.0.1"
databasename="benday-yaml-demo-app"
username="sa"
userPassword="Pa\$\$word"
environment="localdev"

while getopts x:z:s:d:u:e:m:p: flag
do
    case "${flag}" in
        x) dropDatabase=${OPTARG};;
        z) runAdditionalSteps=${OPTARG};;
        s) servername=${OPTARG};;
        d) databasename=${OPTARG};;
        u) username=${OPTARG};;
        p) userPassword=${OPTARG};;
        e) environment=${OPTARG};;
        m) deployEfCoreMigrations=${OPTARG};;
    esac
done

echo "***** DATABASE CREDENTIALS *****";
echo "Drop database: $dropDatabase";
echo "Server name: $servername";
echo "Database name: $databasename";
echo "User name: $username";
echo "Password: $userPassword";
echo "Environment setting: $environment";
echo "Deploy EF migrations: $deployEfCoreMigrations";
echo "Run additional steps: $runAdditionalSteps";
echo "*****";

echo "***** RESTORING DOTNET TOOLS *****"
dotnet tool restore

if [ "$dropDatabase" != "1" ]; then
    echo "***** NOT DROPPING DATABASE *****";
else
    echo "***** DROPPING DATABASE *****"
    sqlcmd -S $servername -Q "drop database if exists [$databasename]" -d master -U $username -P $userPassword
fi

if [ "$deployEfCoreMigrations" != "1" ]; then
    echo "***** NOT DEPLOYING EF CORE MIGRATIONS *****";
else
    cd src/Benday.YamlDemoApp.Api

    echo "***** DEPLOYING EF CORE MIGRATIONS *****"
    dotnet ef database update
fi

cd $base

cd misc/database

echo "***** POPULATING DATABASE PERMISSIONS *****"
./populate-sql-server-permissions.sh -s $servername -d $databasename -u $username -p $userPassword

cd $base

echo "***** UPDATING LOOKUP VALUES *****"
cd misc/database
./update-lookup-values.sh -s $servername -d $databasename -u $username -p $userPassword

cd $base

if [ "$runAdditionalSteps" != "1" ]; then
    echo "***** SKIPPING ADDITIONAL STEPS *****";
else
    echo "***** ADDITIONAL DEPLOYMENT STEPS *****"
    if [ -f ./deploy-additional-steps.sh ]; then
        echo "Found deploy-additional-steps.sh.  Calling it..."
        ./deploy-additional-steps.sh -s $servername -d $databasename -u $username -p $userPassword -e $environment
        cd $base
    else
        echo "INFO: No deploy-additional-steps.sh file found.  FYI, if you want to specify more things to do on deployment, create this file."
    fi
fi

