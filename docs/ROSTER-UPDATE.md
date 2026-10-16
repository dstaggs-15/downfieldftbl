# Roster and run-game update
Open Desktop/Downfield 2.exe.

Clubhouse > Sign Players: nine free agents spanning 70-99 OVR. Credits buy permanent signings. Best signed player for each position group starts automatically next match; WR and OL signings improve their whole position group. Speed, hands and power affect movement, receiving, passing accuracy and blocking. DEF improves opponent-drive outcomes.
Clubhouse > Select Team: eight fictional city teams, including New Orleans Crescent with black/gold uniforms. Selection and roster save next to the existing club save in club.txt.roster. Existing club progress remains compatible.

Right stick while carrying: left/right juke, down spin, up hurdle. Return stick to center before another move. Moves cost stamina and have a cooldown. Right stick while passing still leads throws.
Keyboard equivalents: arrow keys for directional moves, E spin, F dive. Existing Madden-style icon passing, RT sprint, X dive and LB lateral remain.

Y opens 14-call playbook: six pass packages and eight runs (dive, zone, power, counter, sweep, stretch, draw, off tackle). D-pad left/right cycles calls. Calls vary intended lane, block priority and draw mesh timing; steer the runner after handoff.
Carried football is explicitly drawn over the sprite. Sprite caching now tracks possession correctly. Camera follows the football during flight. Hurdles rise visually.

Validation: 42 simulation, 5 analog input, 11 profile, 13 update and 37 native UI checks = 108 passing. UI checks isolate keyboard input from live controller activity. Physical right-stick feel has not been human-tested.
Backup: Downloads/Downfield-v2-backup-before-roster includes original source and desktop executable.
Uses existing Retro Bowl REA research; no fresh REA analysis or proprietary code/assets used.
Right-stick fix: idle keyboard no longer clears XInput aim axes in QB or runner context. Juke uses a timed velocity curve independent of frame rate; verified at 30, 60 and 120 FPS. Spin rotates facing and hurdle/juke have distinct sprite poses. Removed pre-snap defensive coverage disclosure. 114 checks pass. Physical controller playthrough remains unverified.
Fix: preserved controller right-stick axes when keyboard arrows idle; consistent juke at 30/60/120 FPS, spin rotation, hurdle/juke poses. Defensive coverage display removed. 114 checks pass. Desktop corrected executable is Downfield 2 Updated.exe because Windows denied closing the old process and replacing its locked executable.
