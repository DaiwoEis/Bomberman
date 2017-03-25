using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;

    [SerializeField]
    private float _distance = 0f;

    [SerializeField]
    private float _moveSpeed = 1f;

    private void Start()
    {
        _distance = transform.InverseTransformDirection(_target.position - transform.position).z;
    }

    private void LateUpdate()
    {
        if (!Utility.IsArrive(transform.position, GetTargetPosition(_target)))
        {            
            MoveToTarget(_target);
        }
    }

    private void MoveToTarget(Transform target)
    {
        Vector3 direction = GetTargetPosition(target) - transform.position;
        direction.y = 0f;
        direction.Normalize();
        transform.position += direction*_moveSpeed*Time.deltaTime;
    }

    public Vector3 GetTargetPosition(Transform target)
    {
        return target.position - transform.forward*_distance;
    }
}
