using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;

    private float _distance = 0f;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private float _pursueDistance = 2f;

    [SerializeField]
    private float _pursueSpeedCoeff = 5f;

    private void Start()
    {
        _distance = transform.InverseTransformDirection(_target.position - transform.position).z;
    }

    private void LateUpdate()
    {
        float moveSpeed = _moveSpeed;
        Vector3 targetPos = GetTargetPosition(_target);

        if (Utility.SqrtDistance(targetPos, transform.position) > _pursueDistance*_pursueDistance)
        {
            moveSpeed *= _pursueSpeedCoeff;
        }

        if (!Utility.IsArrive(transform.position, GetTargetPosition(_target)))
        {            
            MoveToTarget(_target, moveSpeed);
        }
    }

    private void MoveToTarget(Transform target, float moveSpeed)
    {
        Vector3 direction = GetTargetPosition(target) - transform.position;
        direction.y = 0f;
        direction.Normalize();
        transform.position += direction*moveSpeed*Time.deltaTime;
    }

    public Vector3 GetTargetPosition(Transform target)
    {
        return target.position - transform.forward*_distance;
    }
}
