using System.Collections;
using UnityEngine;

public class TeleportEvents : MonoBehaviour
{

    [SerializeField] private AudioSource _teleportSFX;
    [SerializeField] private SeedSpill _seedSpill;

    public void PlayTeleportSFX()
    {
        _teleportSFX.Play();
    }

    public void DisableSeedSpill()
    {
        if (_seedSpill == null) return;
        
        _seedSpill.enabled = false;
        //StartCoroutine(EnableSeedSpill());
        Invoke(nameof(EnableSeedSpill), 0.5f);
    }

    public void EnableSeedSpill()
    {
        _seedSpill.enabled = true;
    }
}
