using UnityEngine;

[RequireComponent(typeof(AudioSource))]
/// <summary>
/// Description: This class handles the sound effects for an animal.
/// Author: Marc Fischer, Manuel Hagen
/// </summary>
public class AnimalSFX : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _sfxCooldown = 10f;
    [SerializeField][Range(0.0f, 1.0f)] private float _sfxProbability;
    private float _sfxCooldownLast = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _sfxCooldownLast = Time.time;
    }

    void Update()
    {
        // After cooldown of x seconds play animal sfx with a specified probalbility
        if (Time.time - _sfxCooldownLast >= _sfxCooldown)
        {
            if(Random.Range(0f,1f) <= _sfxProbability) {
                _audioSource.Play();
            }
            _sfxCooldownLast = Time.time;
        }
    }
}
