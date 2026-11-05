$ErrorActionPreference = 'Stop'
& (Join-Path $PSScriptRoot 'build.ps1')
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $compiler /nologo ("/reference:" + (Join-Path $PSScriptRoot 'bin\Portfolio.dll')) ("/out:" + (Join-Path $PSScriptRoot 'bin\Checks.exe')) (Join-Path $PSScriptRoot 'tests\Checks.cs')
if ($LASTEXITCODE -ne 0) { throw 'Falhou ao compilar os testes.' }
& (Join-Path $PSScriptRoot 'bin\Checks.exe')
if ($LASTEXITCODE -ne 0) { throw 'Falhou nos testes.' }
