$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$gamePath = Join-Path $root 'build\Downfield.exe'
$installerPath = Join-Path $root 'play me\Install Downfield.exe'
$resolveHandler = [ResolveEventHandler] { param($sender,$eventArgs) [Reflection.Assembly]::ReflectionOnlyLoad($eventArgs.Name) }
[AppDomain]::CurrentDomain.add_ReflectionOnlyAssemblyResolve($resolveHandler)
try {
    $game = [Reflection.Assembly]::ReflectionOnlyLoad([IO.File]::ReadAllBytes($gamePath))
    $testTypes = @($game.GetTypes() | Where-Object {$_.Name.EndsWith('Tests')})
    if ($testTypes.Count -gt 0) { throw 'Test classes leaked into the playable game.' }
    $flags = [Reflection.BindingFlags]'Public,NonPublic,Static,Instance'
    if ($game.GetType('Downfield.Program').GetMethod('InputTests',$flags)) { throw 'Test command handler leaked into the game.' }
    if ($game.GetType('Downfield.GameForm').GetMethod('SavePreview',$flags)) { throw 'Development screenshot writer leaked into the game.' }
    if ($game.GetType('Downfield.Profile').GetField('StorageOverride',$flags)) { throw 'Test save redirection leaked into the game.' }
    foreach ($reference in $game.GetReferencedAssemblies()) {
        if ($reference.Name -notin @('mscorlib','System','System.Core','System.Drawing','System.Windows.Forms','System.Xml')) { throw ('Unexpected game dependency: '+$reference.Name) }
    }
    $version = [Diagnostics.FileVersionInfo]::GetVersionInfo($gamePath)
    if ($version.ProductName -ne 'Downfield 2' -or $version.FileVersion -ne '2.3.2026.1022') { throw 'Missing game product/version metadata.' }
    if (![Text.Encoding]::UTF8.GetString([IO.File]::ReadAllBytes($gamePath)).Contains('level="asInvoker"')) { throw 'Game manifest must request normal user privileges.' }
    $installer = [Reflection.Assembly]::ReflectionOnlyLoad([IO.File]::ReadAllBytes($installerPath))
    if ($installer.GetType('DownfieldSetup.InstallEngine').GetMethod('SelfTest',$flags)) { throw 'Installation test helper leaked into the public installer.' }
    if ($installer.GetManifestResourceNames().Count -ne 4) { throw 'Unexpected installer payload count.' }
    $stream = $installer.GetManifestResourceStream('Downfield.Game')
    $hash = [Security.Cryptography.SHA256]::Create()
    try { $payloadHash=([BitConverter]::ToString($hash.ComputeHash($stream))).Replace('-','') } finally { $stream.Dispose();$hash.Dispose() }
    if ($payloadHash -ne (Get-FileHash -LiteralPath $gamePath -Algorithm SHA256).Hash) { throw 'Installer payload differs from the release game.' }
    if (![Text.Encoding]::UTF8.GetString([IO.File]::ReadAllBytes($installerPath)).Contains('level="asInvoker"')) { throw 'Installer manifest must request normal user privileges.' }
    [PSCustomObject]@{ProductionTestClasses=$testTypes.Count;DevelopmentScreenshotWriter=$false;InstallerTestHelper=$false;Product=$version.ProductName;Version=$version.FileVersion;EmbeddedGameMatches=$true;AdministratorRequired=$false;GameBytes=(Get-Item -LiteralPath $gamePath).Length;GameSHA256=(Get-FileHash -LiteralPath $gamePath -Algorithm SHA256).Hash;InstallerSHA256=(Get-FileHash -LiteralPath $installerPath -Algorithm SHA256).Hash;SignatureStatus=(Get-AuthenticodeSignature -FilePath $gamePath).Status.ToString()} | ConvertTo-Json -Compress
} finally {
    [AppDomain]::CurrentDomain.remove_ReflectionOnlyAssemblyResolve($resolveHandler)
}
