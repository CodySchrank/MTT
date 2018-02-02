#!/usr/bin/env bash

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

__exec dotnet restore ./Source/MTT/
__exec dotnet pack -c Release ./Source/MTT/
__exec dotnet restore ./Example/
__exec dotnet msbuild /nologo '/t:Convert' ./Example/
