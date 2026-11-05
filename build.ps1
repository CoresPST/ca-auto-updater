$ErrorActionPreference = 'Stop'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$output = Join-Path $PSScriptRoot 'bin'
New-Item -ItemType Directory -Force -Path $output | Out-Null
$sources = Get-ChildItem -LiteralPath (Join-Path $PSScriptRoot 'src') -Filter '*.cs' | Select-Object -ExpandProperty FullName
& $compiler /nologo /target:library ("/out:" + (Join-Path $output 'Portfolio.dll')) $sources
if ($LASTEXITCODE -ne 0) { throw 'Falhou ao compilar.' }
