using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HorizontalMove : MonoBehaviour
{
    private Rigidbody _rigidbody = null;

    [SerializeField]
    private Map _map = null;

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private MoveState _currState = MoveState.Null;

    [SerializeField]
    private MoveState _nextState = MoveState.Null;

    private Vector3 _destPos = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        _nextState = MoveState.AtLeft;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void Update()
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
            switch (_nextState)
            {
                case MoveState.AtRight:
                    do
                    {
                        Vector3 leftEndPos = _map.GetCenterPosition(new Vector3(0f, 0f, transform.position.z));
                        int col = _map.GetTile(transform.position).tileIndex.col;
                        col--;
                        while (col >= 0)
                        {
                            if (_map.GetTile(leftEndPos + Vector3.right*col).tileItems.Count > 0)
                                break;
                            col--;
                        }
                        col = Mathf.Clamp(col, 0, _map.GetTile(transform.position).tileIndex.col - 1);
                        _destPos = leftEndPos + Vector3.right*(col + 1);
                    } while (false);
                    break;
                case MoveState.AtLeft:
                    do
                    {
                        Vector3 leftEndPos = _map.GetCenterPosition(new Vector3(0f, 0f, transform.position.z));
                        int col = _map.GetTile(transform.position).tileIndex.col;
                        col++;
                        while (col < _map.width)
                        {
                            if (_map.GetTile(leftEndPos + Vector3.right * col).tileItems.Count > 0)
                                break;
                            col++;
                        }
                        col = Mathf.Clamp(col, _map.GetTile(transform.position).tileIndex.col - 1, _map.width - 1);
                        _destPos = leftEndPos + Vector3.right * (col - 1);
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
                _animator.SetFloat("Speed", 1f);
                break;
            case MoveState.MovingToLeft:
                transform.forward = Vector3.left;
                _rigidbody.velocity = Vector3.left * _moveSpeed;
                _animator.SetFloat("Speed", 1f);
                break;
        }
    }

    private bool MoveComplete()
    {
        return Vector3.Distance(transform.position, _destPos) < 0.1f;
    }

    private bool CheckBlock(Vector3 direction)
    {        
        return Physics.Raycast(transform.position + Vector3.up*0.5f, direction, 1f);
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
