using _Game.Scripts.Modules.SoundManager;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Description:    Manages seed particle system emission based on object transform angle\n
/// Author:         Marc Fischer, Manuel Hagen\n
/// </summary>
public class SeedSpill : MonoBehaviour
{
    [SerializeField] private float _spillAngle = 80.0f;
    [SerializeField] private float _spillVelocity = 3.0f;
    
    private ParticleSystem _seedParticleSystem;
    private ParticleSystem.EmissionModule _emissionModule;

    private Transform _parentTransform; // transform of seedbag model
    private Vector3 _lastPosition = Vector3.zero;
    
    [SerializeField] private AudioSource _seedSpillSFX;
    private bool _playingSpillSFX = false;


    void Start()
    {
        _seedParticleSystem = GetComponent<ParticleSystem>();
        _parentTransform = transform.parent.transform;
        _emissionModule = _seedParticleSystem.emission;
    }

    void Update()
    {
        _seedSpillSFX.volume = (90 - Vector3.Angle(Vector3.down, transform.up)) / 90;

        // enable seed emission when seedbag is held upside down ("upside down" is specified by spillAngle)
        if (Vector3.Angle(Vector3.down, transform.up) <= _spillAngle)
        {
            if (!_playingSpillSFX)
            {
                _seedSpillSFX.Play();
                _playingSpillSFX = true;
            }
            _emissionModule.enabled = true;
            // seeds should be emitted -> no need to continue checking for other emission criteria
            return;
        }

        // calculate velocity of seedbag based on last frames position and current position
        Vector3 velocity = _parentTransform.position - _lastPosition;
        _lastPosition = _parentTransform.position;

        //Debug.Log(Vector3.Dot(velocity, _parentTransform.up) * 100);

        _seedSpillSFX.volume = Mathf.Abs(Vector3.Dot(velocity, _parentTransform.up) * 10);

        // enable seed emission when seedbag is moved towards its opening faster than a given threshold
        if (Mathf.Abs(Vector3.Dot(velocity, _parentTransform.up)*100) > _spillVelocity)
        {
            if (!_playingSpillSFX)
            {
                _seedSpillSFX.Play();
                _playingSpillSFX = true;
            }
            _emissionModule.enabled = true;
            // seeds should be emitted -> no need to continue checking for other emission criteria
            return;
        }

        // if no emission criteria are met, disable emission
        if (_playingSpillSFX)
        {
            _seedSpillSFX.Stop();
            _playingSpillSFX = false;
        }
        _emissionModule.enabled = false;
    }
}
