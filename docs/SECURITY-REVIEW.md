# Reported Defender detection

Status: **under investigation**, October 22, 2026. The reported detection is `Trojan:Win32/Ulthar.A!ml`. The exact affected file and whether the warning occurred on this PC or another tester's PC are still pending. Microsoft has not issued a determination for this report.

Keep flagged copies quarantined. The project owner requested that downloads be restored while the suspected false positive is investigated. The installer, Windows ZIP and portable EXE are available again. This is not a Microsoft clearance. Source and franchise saves are preserved. The release cleanup is complete; Microsoft review remains pending.

## Files being reviewed

The current release is **2.3.2026.1023**. It separates production code from development/test code: the playable EXE excludes all test classes, test command handlers, screenshot writers and test save redirection. The public installer excludes its self-test and screenshot modes; installation tests run in a separate build-only executable. Both EXEs have project product/version metadata and explicitly request normal user privileges. Whether the old test code caused the reported alert remains unverified.

The release game is 1,359,360 bytes, compared with 1,367,040 bytes before the separation. Its SHA-256 is `510c6ce9a51f9b86cb68acc736f5d9782a512ccec14cfc7c41928949f9db6fbf`. The installer SHA-256 is `b7e460d60675a1899c8771d2f90f13eaf94980ede694c50c4d6afffacf266304`. The Windows ZIP SHA-256 is `09911c7972bb4105d39f1a11292648f99557ad052ec6c564769f8892474613c5`.

All 860 game/menu checks and the separate installer checks passed. `verify-release.ps1` verifies that test types, test save redirection, development screenshot methods and the installer's self-test are absent, the embedded game matches, dependencies are expected Windows framework libraries, and manifests request normal privileges. The corresponding game form started successfully in the separate native UI test build. A local Defender custom scan of the rebuilt distribution returned exit 0 and no threats.

The game and installer remain **unsigned**. No code-signing certificate was available in the current-user or local-machine personal certificate stores. Product metadata is not a verified publisher signature. Microsoft documents [trusted signing options](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options), and [developer detection review](https://learn.microsoft.com/en-us/defender-xdr/submission-guide). The technical description for a review is prepared in [MICROSOFT-REVIEW.txt](MICROSOFT-REVIEW.txt); it has not been submitted because Microsoft sign-in is required.

### Original reported release

These are the SHA-256 hashes of the files published at commit `70f224144223698305deea8bb01e011bf6b1baaa`:

| File | SHA-256 |
|---|---|
| Game / desktop EXE | `667c038a5346da0e6e8d0ce9d256b5bc46892e63459f339115af893f9f7c7e53` |
| Install Downfield.exe | `de3dd2e8fe0e51f6f4d130cebbbe64492a00cae723b89fd8764a5b7f678a9908` |
| Downfield-Windows.zip | `3ea8e562e6f8d9259c6bd6fb6a3b0e2e15ecdadd7760252bcded1f0ad11eef61` |

Before release separation, the two local desktop copies, build output and portable game matched that original game hash. The installer's embedded game matched it too. The original package was unsigned.

## Checks completed

- A Microsoft Defender custom scan of the local `play me` folder returned exit code 0 and reported no threats. The scanner was from platform `4.18.26090.9-0`. Reading the installed signature version and detailed threat history was denied in this session. No matching detection events were returned by the available event-log query.
- Source review found no networking, startup registration, credential access, process injection or antivirus-setting code. The game uses Windows XInput for the controller and `winmm.dll` for PCM audio. Save/settings writes target the game's local data files. The installer writes its embedded game and documentation to the app folder and desktop, then launches the game only after the player clicks Play Now.
- A fresh compilation with the Microsoft-signed .NET Framework C# compiler matched all 1,367,040 bytes of the published game except the compiler-generated PE timestamp, module ID and the corresponding generated private-implementation type ID. There were **zero unexpected binary differences**. The game was not executed during this comparison.
- Existing functional tests validate gameplay, saves and installation. They are not malware scans or a security certification.

These findings do not settle the reported detection or establish that every downloaded copy is identical. The rebuilt EXEs have concrete release changes described above. There is no obfuscation, packing, exclusion or protection-setting change to conceal the program or suppress a detection. Flagged quarantine items have not been restored by this work.

## Next evidence needed

1. In Windows Security > Virus & threat protection > Protection history, open the reported detection and record its affected-file path, time, action and definition version if shown.
2. Identify whether the affected file matches one of the hashes above. Do not restore a quarantined item just to hash it.
3. Submit the exact affected file to [Microsoft Security Intelligence](https://www.microsoft.com/en-us/wdsi/filesubmission) for analysis. For a suspected incorrect detection of this game, select the software-developer route and include the detection name, file hash, source commit and these findings. Microsoft's [submission guide](https://learn.microsoft.com/en-us/defender-xdr/submission-guide) says to await the final determination when disputing a program detection. Submission requires signing in; no sample has been submitted from this session.
4. Record Microsoft's submission ID and final determination here. A clean local scan alone does not establish that the detection is a false positive.

Franchise saves live separately under `%LOCALAPPDATA%\Downfield`. They are retained. The production startup check exercised the game's normal save-on-close behavior; no match or team-management action was performed.
