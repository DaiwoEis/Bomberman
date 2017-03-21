using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VerticalMove : Function
{
    private Rigidbody _rigidbody = null;

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private MoveState _currState = MoveState.Null;

    [SerializeField]
    private MoveState _nextState = MoveState.Null;

    private Vector3 _destPos = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Off()
    {
        base.Off();

        _rigidbody.velocity = Vector3.zero;
        _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 0f);
    }

    protected override void OnUpdate()
    {
        switch (_currState)
        {
            case MoveState.MovingToDown:
                if (MoveComplete())
                    _nextState = MoveState.AtDown;
                break;
            case MoveState.MovingToUp:
                if (MoveComplete())
                    _nextState = MoveState.AtUp;
                break;
            case MoveState.AtDown:
                _nextState = MoveState.MovingToUp;
                break;
            case MoveState.AtUp:
                _nextState = MoveState.MovingToDown;
                break;
        }

        if (_nextState != MoveState.Null)
        {
            Map map = Singleton<Map>.instance;
            switch (_nextState)
            {
                case MoveState.AtDown:
                    do
                    {
                        CeilCoordinate coordinate = map.GetCeil(transform.position).coordinate;
                        int row = coordinate.row;
                        for (int i = row + 1; i < map.height; ++i)
                        {
                            if (IsObstacle(map.GetCeil(i, coordinate.col)))
                            {
                                row = i;
                                break;
                            }
                        }
                        if (row <= coordinate.row + 1)
                        {
                            _destPos = map.GetCeil(transform.position).centerPosition;
                        }
                        else
                        {
                            _destPos = map.GetCeil(row - 1, coordinate.col).centerPosition;
                        }
                    } while (false);
                    break;
                case MoveState.AtUp:
                    do
                    {
                        CeilCoordinate coordinate = map.GetCeil(transform.position).coordinate;
                        int row = coordinate.row;
                        for (int i = row - 1; i >= 0; --i)
                        {
                            if (IsObstacle(map.GetCeil(i, coordinate.col)))
                            {
                                row = i;
                                break;
                            }
                        }

                        if (row >= coordinate.row - 1)
                        {
                            _destPos = map.GetCeil(transform.position).centerPosition;
                        }
                        else
                        {
                            _destPos = map.GetCeil(row + 1, coordinate.col).centerPosition;
                        }
                    } while (false);
                    break;
            }
            _currState = _nextState;
            _nextState = MoveState.Null;
        }

        switch (_currState)
        {
            case MoveState.MovingToDown:
                transform.forward = -Vector3.forward;
                _rigidbody.velocity = -Vector3.forward * _moveSpeed;
                _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 1f);
                break;
            case MoveState.MovingToUp:
                transform.forward = Vector3.forward;
                _rigidbody.velocity = Vector3.forward * _moveSpeed;
                _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 1f);
                break;
        }
    }

    private bool IsObstacle(Ceil ceil)
    {
        return ceil.items.Select(c => c.type).Any(type => type == TileItemType.Wall || type == TileItemType.Bomb);
    }

    private bool MoveComplete()
    {
        return Vector3.Distance(transform.position, _destPos) < 0.1f;
    }

    private void OnDrawGizmos()
    {
        if (_destPos != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_destPos + Vector3.up * 2f, 0.3f);
        }
    }

    private enum MoveState
    {
        MovingToDown,
        MovingToUp,
        AtDown,
        AtUp,

        Null
    }
}
