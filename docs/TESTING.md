# Verification - second beta update

441 game checks pass: 42 simulation, 5 input, 11 legacy profile, 19 earlier-update regression, 96 franchise, 22 mechanics, 12 line-play, 50 beta, 33 audio, 16 designer, 33 arcade and 102 native-window UI.

Installer checks also verify fresh installation, byte-identical game and desktop copy, bundled instructions/license, repeat installation and preservation of unrelated files. The packaging script refreshes the ZIP and checksums after the installer passes.

Core output: test-output.txt. Native UI output: ui-test-output.txt. Tests use isolated save directories.

New checks cover capped left-stick placement in both directions, the release input path, both halftime receiving branches, an untimed PAT before halftime, option give/keep, jet-sweep handoff, rating differences, actual WR blocking engagements after a QB crosses the line, custom route movement/copy/save/load, career migration, duplicate-result protection, switching clubs, season archives and all three awards.

Native UI checks navigate Exhibition and Franchise, edit a coach's name and appearance, open the trophy winners, choose a receiver/preset, draw/undo/save/load/use a play, continue halftime and inspect team history. Exhibition simulation is checked against the original franchise values and byte-identical save file. Screenshots include create-play.png, draw-play-freehand.png, menu.png, franchise.png, coach.png, draw-play.png, trophies-won.png, history.png, playoff-field.png and championship-field.png.

The 800-call rush probe produces 637 straight calls and 163 stunts, with no consecutive stunts. Truck checks cover both success and failure, actual knockback, a prone interval and post-whistle animation without changing the dead-ball spot. Other checks include 24,000 randomized frames, trades, contracts, injuries, playoff byes/reseeding and atomic save recovery.

Balance probes: 66/120 defended throws completed; 46 catches gained more than two yards after catch; average prethrow separation 2.7 yards. A repeated sideline sprint scored 0/60 touchdowns and averaged 3.1 yards. In matched open throws, 70 hands caught 147/200 and 99 hands caught 185/200. These are scripted probes, not a full human balance test.

Audio tests verify crowd layering, contact/whistle overlap, delayed whistles, mute, focus loss, bounded voices, no clipping and cue triggers. A separate native playback smoke test opened the Windows device and submitted 184 PCM buffers. Scripted created-play passing completed 12/12 open slants. Human listening and balance review remain useful. Native UI tests isolate keyboard input from XInput. A physical Xbox playthrough remains unverified; the poll found no connected controller.

Build: powershell -ExecutionPolicy Bypass -File .\build.ps1

UI: build\Downfield.Tests.exe --ui-test

Installer/ZIP: powershell -ExecutionPolicy Bypass -File .\build-installer.ps1
The arcade update adds model and native UI checks for the hurdle timing window, mistimed collision penalties, shared breakaway pace, real-time clock accounting, four-second runoff and its exceptions, quarter-ending punts, RS-click throwaways, exact injury recovery, ambulance arrival/loading/skip, reserve replacement, per-channel mute, preference persistence and pause-menu navigation.

A matched 160-contact hurdle probe cleared defenders 131 times with good timing and zero times at the takeoff/landing edges. Fumble or injury penalties occurred in 20 good-timing trials and 81 mistimed trials. The tests use fixed timing and ratings; these are not guaranteed player success rates.

The same 60-seed sideline sprint probe fell from 5.3 to 3.1 yards average gain. The varied passing probe remains close to its previous result: 66/120 completions with 46 catches gaining more than two yards after catch. A separate 100-scene open-field cut/juke probe had 95 stops and 5 touchdowns; the old build had 100 stops. That sample shows a few earned breakaways rather than every tackle being automatic. It does not establish that every human exploit is fixed.

New screenshots: pause-settings.png, quick-mute.png, hurdle-cleared.png, ambulance-arrival.png, ambulance-pickup.png and injuries-serious.png. Physical Xbox feel and human game balance still need playtesting.