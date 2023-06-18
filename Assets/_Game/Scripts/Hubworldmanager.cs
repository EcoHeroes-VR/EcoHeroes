using _Game.Scripts.Modules.SoundManager;
using UnityEngine;

public class Hubworldmanager : MonoBehaviour
{

    [SerializeField] private SoundPackageSo _backgroundSoundpack;
    [SerializeField] private SoundPackageSo _sfxSoundpack;

    void Start()
    {
        SoundManager.GetInstance.BackgroundPackageSo = _backgroundSoundpack;
        SoundManager.GetInstance.SfxPackageSo = _sfxSoundpack;
    }
}
