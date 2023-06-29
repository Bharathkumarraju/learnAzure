#!/bin/bash

base="$PWD"

cd src/Benday.YamlDemoApp.Api

dotnet ef migrations remove

cd $base