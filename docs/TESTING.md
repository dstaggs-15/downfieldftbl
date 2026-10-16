# Verification — line-play and sharing update
272 game checks pass: 42 simulation, 5 input, 11 legacy profile, 19 earlier-update regression, 96 franchise, 22 mechanics, 12 line-play, 65 native-window UI.
Installer checks additionally verify fresh installation, byte-identical game and desktop copy, bundled instructions/license, repeat installation and preservation of unrelated files.
Core output: test-output.txt. Native UI output: ui-test-output.txt.
The 800-call rush probe produced 637 straight calls and 163 stunts, with no consecutive stunts. Stunts use one two-player pair with varied sides. Live-play probes check bounded rusher travel, unique blocking partnerships and blockers staying between the engaged rusher and QB.
Truck tests exercise both random success and failure, require over 1.5 yards of actual knockback, verify a grounded prone interval, continued running after success, post-whistle animation after failure and an unchanged dead-ball spot.
Other checks cover seeded football outcomes, 24,000 randomized simulation frames, analog input, save compatibility, roster transactions, unique round-robin pairings, playoff byes/reseeding, contracts, injuries and atomic backup recovery.
Balance sample: 82/120 defended throws completed; 45 catches gained more than two yards after catch; average prethrow gap 3.1 yards. A fixed sideline-sprint pattern scored 0/60 touchdowns with 2.9 yards average gain. These are scripted probes rather than a full human playtest.
Native UI tests isolate keyboard input from live XInput activity. Physical Xbox controller playthrough remains unverified.
All save and installer checks use isolated temporary directories.
Build: powershell -ExecutionPolicy Bypass -File .\build.ps1
UI: build\Downfield.Tests.exe --ui-test
Installer: powershell -ExecutionPolicy Bypass -File .\build-installer.ps1
Screenshots include truck-win.png, truck-stuffed.png, installer.png, office.png, roster.png, playoffs.png, end-zone.png, kick-windup.png and kick.png.
