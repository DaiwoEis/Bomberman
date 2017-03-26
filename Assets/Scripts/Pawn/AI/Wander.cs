using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Wander : Function
{
    private Rigidbody _rigidbody = null;

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private Vector3[] _moveDirections = null;

    [SerializeField]
    private string[] _obstacleTags = null;

    private Vector3 _destPos = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void On()
    {
        base.On();

        _destPos = Map.instance.GetCenterPosition(transform.position);
    }

    public override void Off()
    {
        base.Off();

        _rigidbody.velocity = Vector3.zero;
        _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 0f);
    }

    protected override void OnUpdate()
    {
        Vector3 moveDirection = transform.forward;

        if (MoveIsComplete())
        {
            moveDirection = GetMoveDirection();
            _destPos = Map.instance.GetCenterPosition(Map.instance.GetCenterPosition(transform.position) + moveDirection);
        }

        Move(moveDirection);
    }

    private void Move(Vector3 moveDirection)
    {
        transform.forward = moveDirection;
        transform.position += moveDirection * _moveSpeed * Time.deltaTime;

        _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 1f);
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 moveDir = transform.forward;

        if (IsObstacle(transform.forward))
        {
            foreach (Vector3 moveDirection in _moveDirections)
            {
                if (!IsObstacle(moveDirection))
                {
                    moveDir = moveDirection;
                    break;
                }
            }
        }
        return moveDir;
    }

    private bool IsObstacle(Vector3 direction)
    {
        return
            Physics.RaycastAll(transform.position + Vector3.up*0.5f, direction, 1f)
                .Any(hit => _obstacleTags.Any(t => t.Equals(hit.collider.tag)));
    }

    private bool MoveIsComplete()
    {
        return Utility.IsArrive(transform.position, _destPos);
    }

    private void OnDrawGizmos()
    {
        if (_destPos != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_destPos + Vector3.up*2f, 0.3f);
        }
    }
}
