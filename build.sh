#!/usr/bin/env bash

# Doing this because running this normally in bash on windows returns this:
# `MSBUILD : error MSB1008: Only one project can be specified.
# Switch: t:ConvertMain`
# and I dont feel like figuring out why
if [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then

    # If on windows runs powershell version of build script instead of sh
    powershell -ExecutionPolicy ByPass -File ./build.ps1

else

    set -eu

    CYAN='\033[0;36m'
    NC='\033[0m'

    __exec() {
        local cmd=${1:0}
        shift
        echo -e "${CYAN} > $cmd $@${NC}"
        $cmd $@
    }

    rm -rf artifacts/
    rm -rf Example/obj/
    rm -rf Source/MTT/obj/

    __exec dotnet test
    __exec dotnet restore ./Source/MTT/
    __exec dotnet pack -c Release ./Source/MTT/
    __exec dotnet restore ./Example/
    __exec dotnet msbuild /nologo '/t:ConvertMain' ./Example/

fi