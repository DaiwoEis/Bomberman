using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HorizontalMove : Function
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
            case MoveState.MovingToRight:
                if (MoveComplete())
                    _nextState = MoveState.AtRight;
                break;
            case MoveState.MovingToLeft:
                if (MoveComplete())
                    _nextState = MoveState.AtLeft;
                break;
            case MoveState.AtRight:
                _nextState = MoveState.MovingToLeft;
                break;
            case MoveState.AtLeft:
                _nextState = MoveState.MovingToRight;
                break;
        }

        if (_nextState != MoveState.Null)
        {
            Map map = Singleton<Map>.instance;
            switch (_nextState)
            {
                case MoveState.AtRight:
                    do
                    {
                        CeilCoordinate coordinate = map.GetCeil(transform.position).coordinate;
                        int col = coordinate.col;
                        for (int i = col - 1; i >= 0; --i)
                        {
                            if (IsObstacle(map.GetCeil(coordinate.row, i)))
                            {
                                col = i;
                                break;                                
                            }
                        }
                        if (col >= coordinate.col - 1)
                        {
                            _destPos = map.GetCeil(transform.position).centerPosition;
                        }
                        else
                        {
                            _destPos = map.GetCeil(coordinate.row, col + 1).centerPosition;
                        }
                    } while (false);
                    break;
                case MoveState.AtLeft:
                    do
                    {
                        CeilCoordinate coordinate = map.GetCeil(transform.position).coordinate;
                        int col = coordinate.col;
                        for (int i = col + 1; i < map.width; ++i)
                        {
                            if (IsObstacle(map.GetCeil(coordinate.row, i)))
                            {
                                col = i;
                                break;
                            }
                        }

                        if (col <= coordinate.col + 1)
                        {
                            _destPos = map.GetCeil(transform.position).centerPosition;
                        }
                        else
                        {
                            _destPos = map.GetCeil(coordinate.row, col - 1).centerPosition;
                        }
                    } while (false);
                    break;
            }
            _currState = _nextState;
            _nextState = MoveState.Null;
        }

        switch (_currState)
        {
            case MoveState.MovingToRight:
                transform.forward = Vector3.right;
                _rigidbody.velocity = Vector3.right*_moveSpeed;
                _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 1f);
                break;
            case MoveState.MovingToLeft:
                transform.forward = Vector3.left;
                _rigidbody.velocity = Vector3.left * _moveSpeed;
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
        MovingToRight,
        MovingToLeft,
        AtRight,
        AtLeft,

        Null
    }
}
