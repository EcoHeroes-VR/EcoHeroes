using UnityEngine;

/// <summary>
/// Description: This class rotates an object towards its velocity direction.
/// Author: Marc Fischer, Manuel Hagen
/// </summary>
public class TurnTowardVelocity : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 500f;
    private Rigidbody _rb;
    private Transform _parentTransform;
    private Transform _tr;

    // Current (local) rotation around the (local) y axis of this gameobject
    private float _currentYRotation = 0f;

    // If the angle between the current and target direction falls below 'fallOffAngle', 'turnSpeed' becomes progressively slower (smothing)
    private const float _fallOffAngle = 90f;
    private const float _magnitudeThreshold = 0.001f;
    private float _step;
    private float _angleDifference;

    // Setup
    void Start()
    {
        _tr = transform;
        _parentTransform = _tr.parent;
        _rb = _parentTransform.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        Vector3 velocity = _rb.velocity;

        // Project velocity onto a plane defined by the 'up' direction of the parent transform
        velocity = Vector3.ProjectOnPlane(velocity, _parentTransform.up);

        // Avoid unnessary jitter
        if (velocity.magnitude < _magnitudeThreshold)
        {
            _angleDifference = 0f;
            return;
        }

        velocity.Normalize();
        Vector3 _currentForward = _tr.forward;
        _angleDifference = GetAngle(_currentForward, velocity, _parentTransform.up);
        float angleFactor = Mathf.InverseLerp(0f, _fallOffAngle, Mathf.Abs(_angleDifference));
        _step = Mathf.Sign(_angleDifference) * angleFactor * Time.deltaTime * _turnSpeed;

        // Clamp step
        if (_angleDifference < 0f && _step < _angleDifference)
            _step = _angleDifference;
        else if (_angleDifference > 0f && _step > _angleDifference)
            _step = _angleDifference;

        // Add step to current y angle
        _currentYRotation += _step;

        // Clamp y angle
        if (_currentYRotation > 360f)
            _currentYRotation -= 360f;
        if (_currentYRotation < -360f)
            _currentYRotation += 360f;

        _tr.localRotation = Quaternion.Euler(0f, _currentYRotation, 0f);
    }

    void OnEnable()
    {
        _currentYRotation = transform.localEulerAngles.y;
    }

    /// <summary>
    /// Description: Calculates the signed angle (ranging from -180 to +180) between two vectors in relation to a plane normal.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="_vector1">First vector</param>
    /// <param name="_vector2">Second vector</param>
    /// <param name="_planeNormal">Plane normal</param>
    /// <returns>Signed angle between the vectors</returns>
    private float GetAngle(Vector3 _vector1, Vector3 _vector2, Vector3 _planeNormal)
    {
        float _angle = Vector3.Angle(_vector1, _vector2);
        float _sign = Mathf.Sign(Vector3.Dot(_planeNormal, Vector3.Cross(_vector1, _vector2)));
        float _signedAngle = _angle * _sign;
        return _signedAngle;
    }
}
