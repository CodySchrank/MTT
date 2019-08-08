$ErrorActionPreference = 'Stop'

function exec($_cmd) {
    write-host " > $_cmd $args" -ForegroundColor cyan
    & $_cmd @args
    if ($LASTEXITCODE -ne 0) {
        throw 'Command failed'
    }
}

Remove-Item artifacts/ -Recurse -ErrorAction Ignore
Remove-Item Example/obj/ -Recurse -ErrorAction Ignore
Remove-Item Source/MTT/obj/ -Recurse -ErrorAction Ignore

exec dotnet test
exec dotnet restore ./Source/MTT/
exec dotnet pack -c Release ./Source/MTT/
exec dotnet restore ./Example/
exec dotnet msbuild /nologo '/t:ConvertMain' ./Example/