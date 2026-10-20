$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
if (!(Test-Path -LiteralPath $compiler)) { throw 'Windows .NET Framework C# compiler is required.' }
$output = Join-Path $projectRoot 'build'
New-Item -ItemType Directory -Force -Path $output | Out-Null
$sources = Get-ChildItem -LiteralPath (Join-Path $projectRoot 'src') -Filter '*.cs' | Select-Object -ExpandProperty FullName
$gameSources = $sources | Where-Object { !(Split-Path -Leaf $_).EndsWith('Tests.cs') }
$manifest = Join-Path $projectRoot 'src\Application.manifest'
$crowdResources = Get-ChildItem -LiteralPath (Join-Path $projectRoot 'audio') -Filter '*.wav' | ForEach-Object { '/resource:' + $_.FullName + ',Downfield.Audio.' + $_.Name }
& $compiler /nologo /optimize+ /target:winexe /platform:x64 /r:System.Drawing.dll /r:System.Windows.Forms.dll "/win32manifest:$manifest" /out:"$output\Downfield.exe" $crowdResources $gameSources
if ($LASTEXITCODE -ne 0) { throw 'Build failed' }
& $compiler /nologo /optimize+ /define:DOWNFIELD_TESTS /target:exe /platform:x64 /r:System.Drawing.dll /r:System.Windows.Forms.dll "/win32manifest:$manifest" /out:"$output\Downfield.Tests.exe" $crowdResources $sources
if ($LASTEXITCODE -ne 0) { throw 'Test build failed' }
& "$output\Downfield.Tests.exe" --test
if ($LASTEXITCODE -ne 0) { throw 'Tests failed' }
Write-Host "Built $output\Downfield.exe"
