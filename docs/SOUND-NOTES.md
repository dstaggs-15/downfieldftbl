# Stadium sounds

Crowd ambience, football cheers, applause and groans now use real CC0 recordings. The background is deliberately quiet; scores and big collisions bring the crowd up. There is no synthetic voice/noise crowd layer. Authors, sources and edits are listed in [audio/CREDITS.md](../audio/CREDITS.md).

Hits, the heavier slam, whistles, kick contacts, horns and menu sounds remain original synthesized effects. Whistles start just after contact. Smaller crowd reactions cannot interrupt a touchdown celebration.

CrowdRecordings.cs reads embedded mono PCM WAV resources. FootballSounds.cs caches those clips and makes the other effects. StadiumMixer.cs mixes them into stereo and applies category gains; WaveOutput.cs streams through Windows waveOut. Every sound is in the standalone EXE.

Pause > Settings controls Master, Crowd, Whistles, Hits + Moves, Stadium Horns and Menu Sounds. Left/right, channel mute and draggable bars work. Whistles default to 25%. Xbox View, keyboard M and the field sound button toggle global mute. Preferences save beside the profile in club.txt.audio.xml. Losing focus silences playback. Leaving a match fades the crowd; practice stays quiet.

39 audio checks cover bounded clips, embedded recordings, quiet background levels, changing cheer energy, layering, delayed whistles, mute, focus and gameplay cues. Native playback opened the Windows device and submitted more than 180 buffers. This verifies playback, not subjective sound quality on speakers or headphones.

Optional checks:

    build\Downfield.Tests.exe --audio-smoke
    build\Downfield.Tests.exe --audio-preview build\Football-stadium-preview.wav