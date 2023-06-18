using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{

    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public float followSpeed;
    public float rotateSpeed;

    [SerializeField] private Transform _followTarget;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.maxAngularVelocity = 20000;
    }

    void FixedUpdate()
    {
        PhysicsMove();
        //_rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        //Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);

        //rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        //if (Mathf.Abs(rotationAxis.magnitude) != Mathf.Infinity)
        //{
        //    if (angleInDegree > 180)
        //    {
        //        angleInDegree -= 360;
        //    }
        //    Vector3 rotationDifferenceInDegrees = angleInDegree * rotationAxis;
        //    _rb.angularVelocity = (rotationDifferenceInDegrees * Mathf.Deg2Rad / Time.fixedDeltaTime);
        //}
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = _followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _rb.velocity = (positionWithOffset - transform.position) / Time.fixedDeltaTime;

        // Rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_rb.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            _rb.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        }
    }
}
