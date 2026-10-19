# Downfield sounds

All sound effects in this build are made by GameAudio.cs. They use short sine/square tones and seeded noise written into PCM WAV data in memory. They are covered by the project's MIT license along with the source.

The game has menu, confirmation, snap, throw, catch, tackle, truck, jump, kick, touchdown, whistle and halftime cues. Options switches sound on/off. Windows plays them through SoundPlayer; there are no audio files to install, network requests or extra audio libraries.

No recordings from Retro Bowl, Madden, Kenney or another GitHub project are included. CC0 packs were considered; this release uses original synthesis instead. This avoids requiring a separately downloaded sound pack.

Automated checks verify the WAV headers/data sizes, cue handling and mute switch. Native UI checks run with playback suppressed. Actual speaker/headphone playback still needs a human listen on the target PC.
