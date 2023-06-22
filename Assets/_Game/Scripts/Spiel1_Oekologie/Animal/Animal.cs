//#define DEBUG_STATES
//#define DEBUG_VELOCITY
//#define DEBUG_ACCELERATION
//#define DEBUG_COLLISIONS
//#define DEBUG_TRIGGER
//#define DEBUG_PARTICLES

using UnityEngine;
using System.Collections;

/// <summary>
/// Description: This script controls the behavior of an animal in a game.
/// Author: Marc Fischer, Manuel Hagen
/// </summary>
public class Animal : MonoBehaviour
{
    private Rigidbody _rb;
    private Level1GameManager _gameManager;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private Transform _farmlandCenterPoint;

    // Heart particle variables
    [SerializeField] private ParticleSystem _heartParticles;
    private ParticleSystem.EmissionModule _heartParticlesEmissionModule;
    private IEnumerator _heartParticleCoroutine;
    [SerializeField] private float _heartParticleRateLow = 1f;
    [SerializeField] private float _heartParticleRateHigh = 3f;
    [SerializeField] private float _petCooldownSeconds = 2f;
    private bool _heartParticlesCoroutineActive = false;

    [SerializeField] private CapsuleCollider[] petCapsuleCollider;
    [SerializeField] private float petThresholdSeconds = 2f;
    [SerializeField] private float petThresholdSecondsHard = 5f;
    private float currentPetTime = 0;

    [SerializeField] private AnimalState _currentState;
    private Vector3 _targetPosition;

    private Vector3 _lastPosition;
    private float _currentVelocity;
    [SerializeField] private float _velocityTrackingPointPositionOffset;


    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _attackCooldown = 10f; //Number of Ticks per second
    private float _attackTimeSinceLastTry = 0f;
    private float _distanceSpeedMultiplier;
    [SerializeField] private float _turnSpeedMultiplier = 3f;

    // Eating Variables
    [SerializeField] private float _eatDistance = 1f;
    [SerializeField] private float _timeToEat = 0.5f;
    private bool _hasEaten;

    // Idle Variables
    [SerializeField] private float _idleMoveSpeed = 2f;
    [SerializeField] private float _maxIdleMoveDistance;
    private Vector3 _lastIdlePosition;
    private Vector3 _currentRandomDirection;
    private float _currentAcceleration = 0f;
    private bool _idleWalking = false;
    [SerializeField] private float _accelerationSpeed = 1f;

    private enum AnimalState
    {
        Idle,
        Approach,
        Eat,
        Leave
    }

    private void Start()
    {
        if(_gameManager == null)
        {
            _gameManager = Level1GameManager.GetInstance();
        }
        _currentState = AnimalState.Idle;
        _rb = GetComponent<Rigidbody>();
        _lastPosition = transform.position + transform.forward * _velocityTrackingPointPositionOffset;
        
        //  init heart particle system with correct start values
        _heartParticlesEmissionModule = _heartParticles.emission;
        _heartParticlesEmissionModule.enabled = false;
        _heartParticlesEmissionModule.rateOverTime = 1f;

        if (_gameManager.hardMode)
        {
            petThresholdSeconds = petThresholdSecondsHard;
        }
    }

    /// <summary>
    /// Description: Updates the animal's behavior and state based on its current state.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    private void Update()
    {
        switch (_currentState)
        {
            case AnimalState.Idle:
                _attackTimeSinceLastTry += Time.deltaTime;
                if (_attackTimeSinceLastTry > _attackCooldown)
                {
                    Vector3? targetPosition = _gameManager.GetTargetPosition();
                    if (targetPosition != null)
                    {
                        _targetPosition = targetPosition.Value;
                        _currentState = AnimalState.Approach;
                    }
                    _attackTimeSinceLastTry = 0;
                }
                break;
            case AnimalState.Approach:
                float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_targetPosition.x, 0, _targetPosition.z));
                _distanceSpeedMultiplier = Mathf.Clamp(distance-1/4, 0, 1);

                if (distance < _eatDistance)
                {
                    _currentState = AnimalState.Eat;
                    _animator.SetTrigger("eat");
                    _rb.isKinematic = true;
                }
                break;
            case AnimalState.Eat:
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Eat") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > _timeToEat)
                {
                    if (!_hasEaten)
                    {
                        _gameManager.Eat(_targetPosition);
                        _hasEaten = true;
                    }
                    _rb.isKinematic = false;
                }
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Eat") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    _currentState = AnimalState.Idle;
                    _animator.SetTrigger("idle");
                    _hasEaten = false;
                }
                break;
        }
    }

    /// <summary>
    /// Description: Performs the movement and behavior logic for the animal based on its current state.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case AnimalState.Idle:
#if DEBUG_STATES
Debug.Log("Idle");
#endif
                if (!_idleWalking)
                {
                    _idleWalking = true;
                    _currentRandomDirection = new Vector3(Random.Range(-0.3f, 1f), 0, Random.Range(-0.3f, 1f)).normalized;
                    _lastIdlePosition = transform.position;
                }
                if (_idleWalking)
                {
                    if (IsHittingWall())
                    {
#if DEBUG_COLLISIONS
Debug.Log("Collided with head - ouch :o");
#endif
                        _currentRandomDirection = -_modelTransform.forward;
                        Debug.DrawRay(_raycastPoint.position, -_modelTransform.forward, Color.yellow);
                    }
                    _currentAcceleration = Mathf.Lerp(_currentAcceleration, _idleMoveSpeed, _accelerationSpeed/10);
#if DEBUG_ACCELERATION
Debug.Log(_currentAcceleration);
#endif
                    _rb.velocity = _currentRandomDirection * _currentAcceleration;
                }
                if (Vector3.Distance(_lastIdlePosition, transform.position) >= _maxIdleMoveDistance)
                {
                    _idleWalking = false;
                    _currentAcceleration = 0f;
                }
                break;
            case AnimalState.Approach:
#if DEBUG_STATES
Debug.Log("Approach");
#endif
                Vector3 direction = (_targetPosition - transform.position + _modelTransform.forward * _turnSpeedMultiplier).normalized;
                if (IsHittingWall())
                {
#if DEBUG_COLLISIONS
Debug.Log("Collided with head - ouch :o");
#endif
                    direction += _modelTransform.right;
                    Debug.DrawRay(_raycastPoint.position, -_modelTransform.forward, Color.yellow);
                }
                direction.y = 0;
                _rb.velocity = _distanceSpeedMultiplier * _moveSpeed * direction;
#if DEBUG_VELOCITY
Debug.Log(rb VELOCITY: " + _rb.velocity);
#endif
                break;
            case AnimalState.Eat:
#if DEBUG_STATES
Debug.Log("Eat");
#endif
                break;
            case AnimalState.Leave:
#if DEBUG_STATES
Debug.Log("Leave");
#endif
                _currentRandomDirection = (transform.position - _farmlandCenterPoint.position).normalized;
                _currentAcceleration = Mathf.Lerp(_currentAcceleration, _idleMoveSpeed, _accelerationSpeed / 10);
#if DEBUG_ACCELERATION
Debug.Log(_currentAcceleration);
#endif
                _rb.velocity = _currentRandomDirection * _currentAcceleration;
#if DEBUG_VELOCITY
Debug.Log(_rb.velocity);
#endif
                break;
        }
        Vector3 offsetPoint = transform.forward * _velocityTrackingPointPositionOffset;
        _currentVelocity = Vector3.Distance(transform.position+offsetPoint, _lastPosition) * Time.deltaTime;
        _lastPosition = transform.position + offsetPoint;
        _animator.SetFloat("velocity", _currentVelocity*100000);
    }

    /// <summary>
    /// Description: Checks if the raycast from the '_raycastPoint' is hitting a wall within a specified distance.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <returns>True if hitting a wall, false otherwise</returns>
    private bool IsHittingWall()
    {
        Debug.DrawRay(_raycastPoint.position, _raycastPoint.forward * 0.25f, Color.red, 0.08f);
        return Physics.Raycast(_raycastPoint.position, _raycastPoint.forward, 0.25f);
    }


    /// <summary>
    /// Description: Handles the event when a hand collider stays within the trigger area of the animal.
    ///              If no heart particle coroutine is active, it triggers the petting action.
    ///              If the animal is in the "Idle" state, it transitions to the "Leave" state.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="other">The collider that is staying within the trigger</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand") && !_heartParticlesCoroutineActive)
        {
#if DEBUG_PARTICLES
Debug.Log("PET ANIMAL");
#endif
            Pet();
        }
#if DEBUG_STATES
Debug.Log("ON FIELD");
#endif
        if (_currentState == AnimalState.Idle)
        {
            _currentState = AnimalState.Leave;
        }
    }

    /// <summary>
    /// Description: Handles the event when a hand collider exits the trigger area of the animal.
    ///              If no heart particle coroutine is active, it stops emitting heart particles.
    ///              If the animal is in the "Leave" state, it transitions back to the "Idle" state.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="other">The collider that exited the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand") && !_heartParticlesCoroutineActive)
        {
#if DEBUG_PARTICLES
Debug.Log("STOP PARTICLES");
#endif
            _heartParticlesEmissionModule.enabled = false;
        }
#if DEBUG_STATES
Debug.Log("FIELD LEFT");
#endif
        if (_currentState == AnimalState.Leave)
        {
            _currentState = AnimalState.Idle;
        }
    }

    /// <summary>
    /// Description: Disables the heart particles after a certain wait time.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="waitTime">The time to wait before disabling the particles</param>
    private IEnumerator DisableHeartParticles(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _heartParticlesEmissionModule.enabled = false;
        _heartParticlesEmissionModule.rateOverTime = _heartParticleRateLow;
        _heartParticlesCoroutineActive = false;
#if DEBUG_PARTICLES
Debug.Log("COROUTINE DONE");
#endif
    }

    /// <summary>
    /// Description: Handles the petting action on the animal.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    private void Pet()
    {
        _heartParticlesEmissionModule.enabled = true;

        if (currentPetTime >= petThresholdSeconds)
        {
#if DEBUG_PARTICLES
Debug.Log("ANIMAL PET SUCCESSFUL");
#endif
            _attackTimeSinceLastTry = 0f;
            // increased number of particles when pet effect happens
            _heartParticlesEmissionModule.rateOverTime = _heartParticleRateHigh;
            // turn off heart particle system after x seconds
            _heartParticleCoroutine = DisableHeartParticles(_petCooldownSeconds);
            _heartParticlesCoroutineActive = true;
            StartCoroutine(_heartParticleCoroutine);
#if DEBUG_PARTICLES
Debug.Log("COROUTINE STARTED");
#endif

            // transition to idle state and "put back" target field -> forget the will to eat
            _currentState = AnimalState.Idle;
            _animator.SetTrigger("idle");
            _hasEaten = false;
            _rb.isKinematic = false;
            _gameManager.StopEating(_targetPosition);

            currentPetTime = 0f;
            return;
        }
        currentPetTime += Time.deltaTime;
#if DEBUG_PARTICLES
        Debug.Log("PET ANIMAL FOR: " + currentPetTime + " SECONDS");
#endif
    }
}
