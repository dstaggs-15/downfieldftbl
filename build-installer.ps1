$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $MyInvocation.MyCommand.Path
if (Test-Path -LiteralPath (Join-Path $root 'play me\DOWNLOADS-ON-HOLD.txt')) { throw 'Distribution is paused pending review of a reported Windows Defender detection. See docs/SECURITY-REVIEW.md.' }
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$game = Join-Path $root 'build\Downfield.exe'
if (!(Test-Path -LiteralPath $game)) { throw 'Run build.ps1 first.' }
$play = Join-Path $root 'play me'
$portable = Join-Path $play 'Downfield'
New-Item -ItemType Directory -Force -Path $portable | Out-Null
Copy-Item -LiteralPath $game -Destination (Join-Path $portable 'Downfield.exe') -Force
Copy-Item -LiteralPath (Join-Path $root 'play me\START HERE.txt') -Destination (Join-Path $portable 'START HERE.txt') -Force
Copy-Item -LiteralPath (Join-Path $root 'LICENSE.txt') -Destination (Join-Path $portable 'LICENSE.txt') -Force
Copy-Item -LiteralPath (Join-Path $root 'docs\FRANCHISE-GUIDE.md') -Destination (Join-Path $portable 'FRANCHISE-GUIDE.md') -Force
$output = Join-Path $play 'Install Downfield.exe'
& $compiler /nologo /optimize+ /target:winexe /platform:x64 /r:System.Drawing.dll /r:System.Windows.Forms.dll "/resource:$game,Downfield.Game" "/resource:$root\play me\START HERE.txt,Downfield.Readme" "/resource:$root\docs\FRANCHISE-GUIDE.md,Downfield.Guide" "/resource:$root\LICENSE.txt,Downfield.License" "/out:$output" (Join-Path $root 'installer\Installer.cs')
if ($LASTEXITCODE -ne 0) { throw 'Installer build failed' }
$test = Start-Process -FilePath $output -ArgumentList '--self-test' -WindowStyle Hidden -Wait -PassThru
if ($test.ExitCode -ne 0) { throw 'Installer self-test failed' }
Write-Host 'Installer verified: embedded game, desktop copy, guide/license, repeat install and save preservation.'
$archive = Join-Path $play 'Downfield-Windows.zip'
Compress-Archive -LiteralPath @($portable, $output, (Join-Path $play 'START HERE.txt')) -DestinationPath $archive -Force
$hashes = foreach ($file in @($output, $archive, (Join-Path $portable 'Downfield.exe'))) {
    $relative = $file.Substring($play.Length + 1).Replace('\', '/')
    (Get-FileHash -LiteralPath $file -Algorithm SHA256).Hash.ToLowerInvariant() + '  ' + $relative
}
[IO.File]::WriteAllLines((Join-Path $play 'checksums.sha256'), $hashes)
Write-Host 'Windows ZIP and SHA-256 checksums refreshed.'
