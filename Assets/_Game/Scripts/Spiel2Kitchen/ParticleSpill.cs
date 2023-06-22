using UnityEngine;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    Class to handle at which angle the emission of particles from a particleSystem are enabled.\n
    /// Author:         Theresa Mayer\n
    /// </summary>
    public class ParticleSpill: MonoBehaviour
    {
        [SerializeField] private float angle = 80f;
        private ParticleSystem _particleSystem;
        private ParticleSystem.EmissionModule _emissionModule;
        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _emissionModule = _particleSystem.emission;
        }
        void Update()
        {
            
            if (Vector3.Angle(Vector3.down, transform.up) <= angle)
            {
                _emissionModule.enabled = true;
            }
            else
            {
                _emissionModule.enabled = false;
            }
        }
    }
}
