# Download hold: reported Defender detection

Status: **under investigation**, October 20, 2026. The reported detection is `Trojan:Win32/Ulthar.A!ml`. The exact affected file and whether the warning occurred on this PC or another tester's PC are still pending. Microsoft has not issued a determination for this report.

Keep flagged copies quarantined. Downloads are paused, and the installer, Windows ZIP and portable game have been withdrawn from the current GitHub branch. Historical Git commits can still contain those files; they are not recommended downloads during this review. Source and franchise saves are preserved. Packaging now stops while `play me/DOWNLOADS-ON-HOLD.txt` exists.

## Files being reviewed

These are the SHA-256 hashes of the files published at commit `70f224144223698305deea8bb01e011bf6b1baaa`:

| File | SHA-256 |
|---|---|
| Game / desktop EXE | `667c038a5346da0e6e8d0ce9d256b5bc46892e63459f339115af893f9f7c7e53` |
| Install Downfield.exe | `de3dd2e8fe0e51f6f4d130cebbbe64492a00cae723b89fd8764a5b7f678a9908` |
| Downfield-Windows.zip | `3ea8e562e6f8d9259c6bd6fb6a3b0e2e15ecdadd7760252bcded1f0ad11eef61` |

The two local desktop copies, build output and portable game match that game hash. The installer's embedded game matches it too. The package is unsigned.

## Checks completed

- A Microsoft Defender custom scan of the local `play me` folder returned exit code 0 and reported no threats. The scanner was from platform `4.18.26090.9-0`. Reading the installed signature version and detailed threat history was denied in this session. No matching detection events were returned by the available event-log query.
- Source review found no networking, startup registration, credential access, process injection or antivirus-setting code. The game uses Windows XInput for the controller and `winmm.dll` for PCM audio. Save/settings writes target the game's local data files. The installer writes its embedded game and documentation to the app folder and desktop, then launches the game only after the player clicks Play Now.
- A fresh compilation with the Microsoft-signed .NET Framework C# compiler matched all 1,367,040 bytes of the published game except the compiler-generated PE timestamp, module ID and the corresponding generated private-implementation type ID. There were **zero unexpected binary differences**. The game was not executed during this comparison.
- Existing functional tests validate gameplay, saves and installation. They are not malware scans or a security certification.

These findings do not settle the reported detection or establish that every downloaded copy is identical. No new EXE has been published to change its hash or sidestep the warning. Defender exclusions, restored quarantine items and disabled protection are not part of this investigation.

## Next evidence needed

1. In Windows Security > Virus & threat protection > Protection history, open the reported detection and record its affected-file path, time, action and definition version if shown.
2. Identify whether the affected file matches one of the hashes above. Do not restore a quarantined item just to hash it.
3. Submit the exact affected file to [Microsoft Security Intelligence](https://www.microsoft.com/en-us/wdsi/filesubmission) for analysis. For a suspected incorrect detection of this game, select the software-developer route and include the detection name, file hash, source commit and these findings. Microsoft's [submission guide](https://learn.microsoft.com/en-us/defender-xdr/submission-guide) says to await the final determination when disputing a program detection. Submission requires signing in; no sample has been submitted from this session.
4. Record Microsoft's submission ID and final determination here before lifting the hold. A clean local scan alone does not lift it.

Franchise saves live separately under `%LOCALAPPDATA%\Downfield`. They have not been modified by this review.
