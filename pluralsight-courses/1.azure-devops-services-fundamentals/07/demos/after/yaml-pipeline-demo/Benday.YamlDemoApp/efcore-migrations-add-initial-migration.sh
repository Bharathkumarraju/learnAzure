#!/bin/bash

base="$PWD"

cd src/Benday.YamlDemoApp.Api

dotnet ef migrations add InitialSetup

cd $base