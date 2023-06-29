#!/bin/bash

base="$PWD"
solutionRoot="$base/Benday.YamlDemoApp"
deployScriptsFolder="deploy-scripts"

if [ -d "$deployScriptsFolder" ]; then rm -Rf $deployScriptsFolder; fi

mkdir "$deployScriptsFolder"

cp $solutionRoot/*.sh ./$deployScriptsFolder

cp -r $solutionRoot/misc ./$deployScriptsFolder


