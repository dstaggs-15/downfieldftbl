# Verification

**576 automated checks pass:** 42 simulation, 5 input, 11 legacy profile, 19 earlier-update regression, 96 franchise, 22 mechanics, 12 line-play, 50 beta, 39 audio, 16 designer, 33 arcade, 26 onside, 18 economy, 13 club-update, 14 personnel, 24 defensive-balance and 136 native-window UI.

Core and UI checks use isolated save directories. Output: test-output.txt and ui-test-output.txt. The installer checks fresh installation, byte-identical game and desktop copy, bundled guide/license, reinstall and preservation of saves and unrelated files. Packaging refreshes the ZIP and checksums afterward.

Production and test binaries are now separate. `build.ps1` excludes `*Tests.cs` from the game and defines `DOWNFIELD_TESTS` only for the test runner. Screenshot methods and test save redirection are also conditional. Installer tests use `DOWNFIELD_INSTALLER_TESTS` in a separate build-only EXE. `verify-release.ps1` checks those boundaries, version metadata, normal-user manifests, allowed framework dependencies and the embedded payload. It inspects the assemblies without running them. The rebuilt distribution passed a local Defender custom scan; the reported Ulthar detection still needs Microsoft's review. See [SECURITY-REVIEW.md](SECURITY-REVIEW.md).

The 24 defensive-balance checks cover safety depth, vertical route matching, grounded pursuit, upgraded rosters, useful short throws/custom routes, and 120 complete games with normal kickoffs or repeated onside attempts. See [DEFENSE-BALANCE.md](DEFENSE-BALANCE.md) for matched before/after measurements and the limits of those probes.

The earlier checks play through drag-to-draw and controller route drawing, independent save/load, an actual RB pitch back and QB forward pass, reserve jersey identities, an empty RB spot, a wildcat QB without cloning a player, and unique persistent roster numbers. The 20-seed pitch-back probe returned 14 balls to the QB; drops and loose balls remain possible.

Onside checks cover the first-use explanation and frozen clock, fourth-and-13 at the own 30, the own-43 conversion line, exact turnover spots, no repeat explanation, all quarters while leading, PAT/field-goal kickoff choices, regular kickoffs and new-game reset. Kicking or simulating a whole drive cannot bypass the attempt.

Business checks cover game and bye expenses, duplicate-result protection, independent food/parking prices, limits, initial sales versus lost fan support, saved prices/fans/job security, firing after a grace period, available jobs and the unchanged career record after hiring elsewhere. A perfect regular season earns less than $55 net before spending.

All 12 teams produce award candidates. Stat categories sort actual yards. Matched simulated offenses produce more passing with better ratings. Receiving yards, catches and touchdowns reconcile with QB totals. Final MVP and OPOTY winners match the visible rankings, and winner profiles survive save/load. Simulated stats are estimates based on ratings, matchup, role and game variation.

Spectacular-hit checks verify launches of several yards, recovery, heavy contacts from both sides and unchanged dead-ball spots. The 800-call rush probe remains 637 straight rushes / 163 stunts, without consecutive stunts. The repeated 60-seed sideline run averages 2.9 yards with no touchdowns. Defended passing remains 64/120 completions, 39 gaining more than two yards after catch, with 3.0 yards average prethrow separation. These scripts do not establish that every human exploit is fixed.

The 160-contact hurdle probe clears 131 defenders with good timing and none at the takeoff/landing edges. Fumble/injury penalties occur in 20 well-timed trials and 90 mistimed trials under the updated injury rates. Outcomes depend on timing, ratings and support.

Audio checks verify embedded recordings, a quiet ambient bed, reaction dynamics and the mixer. Native playback opened the Windows device and submitted more than 180 buffers. Subjective listening remains useful. XInput polling found no connected Xbox controller; physical controller feel remains unverified.

New screenshots show the office, stadium pricing, signing profiles, MVP/OPOTY races, passing leaders, firing/jobs, route drawing, onside explanation/attempt/kickoff choice, spectacular hits, reserve substitution, short-handed offense and both stages of a flea-flicker.

Build and core checks:

    powershell -ExecutionPolicy Bypass -File .\build.ps1

Native UI:

    .\build\Downfield.Tests.exe --ui-test

Installer and ZIP:

    powershell -ExecutionPolicy Bypass -File .\build-installer.ps1
