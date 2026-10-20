# Stadium sounds

Crowd ambience, football cheers, applause and groans now use real CC0 recordings. The background is deliberately quiet; scores and big collisions bring the crowd up. There is no synthetic voice/noise crowd layer. Authors, sources and edits are listed in [audio/CREDITS.md](../audio/CREDITS.md).

Hits, the heavier slam, kick contacts, horns and menu sounds remain original synthesized effects. The old high-pitched whistle is removed; play endings have a short, low contact marker. Smaller crowd reactions cannot interrupt a touchdown celebration. The stadium recording is filtered to remove piercing tones and blended into a quiet 35-second evolving background.

CrowdRecordings.cs reads embedded mono PCM WAV resources. FootballSounds.cs caches those clips and makes the other effects. StadiumMixer.cs mixes them into stereo and applies category gains; WaveOutput.cs streams through Windows waveOut. Every sound is in the standalone EXE.

Pause > Settings controls Master, Crowd, Play Ends, Hits + Moves, Stadium Horns and Menu Sounds. Left/right, channel mute and draggable bars work. Play Ends uses the previous whistle channel so saved volume preferences still work. Xbox View, keyboard M and the field sound button toggle global mute. Preferences save beside the profile in club.txt.audio.xml. Losing focus silences playback. Leaving a match fades the crowd; practice stays quiet.

42 audio checks cover bounded clips, embedded recordings, the longer quiet background, suppression of high whistle tones, changing cheer energy, layering, mute, focus and gameplay cues. Native playback opened the Windows device and submitted more than 180 buffers. This verifies playback, not subjective sound quality on speakers or headphones.

Optional checks:

    build\Downfield.Tests.exe --audio-smoke
    build\Downfield.Tests.exe --audio-preview build\Football-stadium-preview.wav
