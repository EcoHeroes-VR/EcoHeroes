# SoundManager
The soundmanager consists of various objects:
- **SoundManager**: Manages all possible sounds and is therefore the main component.
- **Sound Package So**: Scriptable Object, contains a list of audio elements <br> Create under *Assets/Create/_Game/Music Package SO*
- **AudioElement**: Contains a name and the sound example with a few settings.

> It is possible to change the "Sound Package So" during runtime (e.g. when a scene is changed). <br>
> ```SoundManager.GetInstance.BackgroundPackageSo = package``` <br>
> ```SoundManager.GetInstance.SfxPackageSo = package```


## Background (atmospheric) Sound
To play background sound, the sample to be played must be stored in a "Sound Package So" and assigned to the SoundManager.<br>
Use ```SoundManager.GetInstance.StartBackground("name")``` to play a specific background sound, ```SoundManager.GetInstance.StartRandomBackground()``` to play a random background sound from the list.
Use ```SoundManager.GetInstance.StopBackground()``` to stop the music.

## SFX
To play a SFX sound, use the code: ```SoundManager.GetInstance.StartSfx("name")``` <br>
To stop a SFX sound, use the code: ```SoundManager.GetInstance.StopSfx("name")``` <br><br>
You can start different sfx sounds at the same time

## Dialog
To play a dialogue, use the code: ```SoundManager.GetInstance.StartDialogSequenz(<AudioElement>)```

## Events
> When a sound is finished playing or aborted, an event is triggered. Use
> ```SoundManager.GetInstance.OnAudioEnd.AddListener()``` to listen the event.
> -  First parameter (string) is the name of sound is stopped
> -  Second parameter (AudioArt) is an enum. represent the art of sound (Sfx, Background, Dialog)