using UnityEngine;

/// <summary>
/// Description: This class handles the heart particle system for an animal.
/// Author: Marc Fischer, Manuel Hagen
/// </summary>
public class HeartAnimalParticles : MonoBehaviour
{

    // heart particle variables
    [SerializeField] private ParticleSystem _heartParticles;
    private ParticleSystem.EmissionModule _heartParticlesEmissionModule;

    void Start()
    {
        // init heart particle system with correct start values
        _heartParticlesEmissionModule = _heartParticles.emission;
        _heartParticlesEmissionModule.enabled = false;
        _heartParticlesEmissionModule.rateOverTime = 3f;
    }

    /// <summary>
    /// Description: Activates the heart particles emission when a hand collider stays inside the trigger collider.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="other">The collider entering the trigger</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _heartParticlesEmissionModule.enabled = true;
        }
    }

    /// <summary>
    /// Description: Deactivates the heart particles emission when a hand collider exits the trigger collider.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="other">The collider exiting the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _heartParticlesEmissionModule.enabled = false;
        }
    }
}
