# Verification - second beta update

342 game checks pass: 42 simulation, 5 input, 11 legacy profile, 19 earlier-update regression, 96 franchise, 22 mechanics, 12 line-play, 50 beta and 85 native-window UI.

Installer checks also verify fresh installation, byte-identical game and desktop copy, bundled instructions/license, repeat installation and preservation of unrelated files. The packaging script refreshes the ZIP and checksums after the installer passes.

Core output: test-output.txt. Native UI output: ui-test-output.txt. Tests use isolated save directories.

New checks cover capped left-stick placement in both directions, the release input path, both halftime receiving branches, an untimed PAT before halftime, option give/keep, jet-sweep handoff, rating differences, actual WR blocking engagements after a QB crosses the line, custom route movement/copy/save/load, career migration, duplicate-result protection, switching clubs, season archives and all three awards.

Native UI checks navigate Exhibition and Franchise, edit a coach's name and appearance, open the trophy winners, draw/undo/save/load/run a play, continue halftime and inspect team history. Exhibition simulation is checked against the original franchise values and byte-identical save file. Screenshots include menu.png, franchise.png, coach.png, draw-play.png, trophies-won.png, history.png, playoff-field.png and championship-field.png.

The 800-call rush probe produces 637 straight calls and 163 stunts, with no consecutive stunts. Truck checks cover both success and failure, actual knockback, a prone interval and post-whistle animation without changing the dead-ball spot. Other checks include 24,000 randomized frames, trades, contracts, injuries, playoff byes/reseeding and atomic save recovery.

Balance probes: 65/120 defended throws completed; 47 catches gained more than two yards after catch; average prethrow separation 2.7 yards. A repeated sideline sprint scored 0/60 touchdowns and averaged 5.3 yards. In matched open throws, 70 hands caught 147/200 and 99 hands caught 185/200. These are scripted probes, not a full human balance test.

WAV structure, sound cues and mute are checked. Test playback is suppressed; a human still needs to listen to the sounds. Native UI tests isolate keyboard input from XInput. A physical Xbox playthrough remains unverified; the poll found no connected controller.

Build: powershell -ExecutionPolicy Bypass -File .\build.ps1

UI: build\Downfield.Tests.exe --ui-test

Installer/ZIP: powershell -ExecutionPolicy Bypass -File .\build-installer.ps1