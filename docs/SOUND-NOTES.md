# Stadium sounds

The game now mixes a continuous stadium crowd with football events. Touchdowns get a larger cheer and horn; first downs get applause; big hits get a crowd surge; interceptions, lost fumbles and missed kicks get a disappointed reaction. Simulated results also trigger the appropriate crowd response.

Hits use short low thumps, fabric-like noise and stereo variation. Whistles start just after contact so the hit remains audible. Crowd reactions last several seconds and play alongside other effects. Smaller cheers cannot interrupt a touchdown celebration.

These are original synthesized effects, not stadium recordings. FootballSounds.cs combines layered vowel-like voices, filtered noise, scattered claps and a short echo. The source and generated audio are under the project's MIT license. No Retro Bowl, Madden or third-party audio is included.

StadiumMixer.cs combines the effects and ambience, limits voice count and controls output level. WaveOutput.cs streams stereo PCM through Windows' built-in waveOut API. There are no audio files, extra codecs or downloads to install. [Windows waveform output documentation](https://learn.microsoft.com/en-us/windows/win32/api/mmeapi/nf-mmeapi-waveoutwrite).

Pause > Settings controls Master, Crowd, Whistles, Hits + Moves, Stadium Horns and Menu Sounds independently. Left/right, channel mute and draggable mouse bars are supported. Whistles default to 25% of their channel level. Xbox View, keyboard M and the field sound button toggle global mute. Preferences save beside the profile in club.txt.audio.xml. Options also keeps its global mute switch. Losing focus clears effects and silences playback. Leaving the match fades the crowd out; practice has no crowd ambience.

33 audio checks cover clip bounds, overlap, delayed whistles, crowd priority/volume, mute, focus loss, voice lifetime and actual gameplay cues. A native smoke run opened the available Windows output device and submitted 184 buffers successfully. This verifies the output path, not subjective sound quality or speaker/headphone volume.

Optional checks:
    build\Downfield.Tests.exe --audio-smoke
    build\Downfield.Tests.exe --audio-preview build\Football-stadium-preview.wav