using System.Linq;
using UnityEngine;

public class PlayerMoveController : Function 
{
    private Character _character = null;

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private Border _currCeilLimtBorder = null;

    [SerializeField]
    private string[] _obstacleTags = null;

    [SerializeField]
    private float _coillideRadius = 0.3f;

    private Ceil _currCeil = null;

    protected override void Awake()
    {
        base.Awake();

        _character = GetComponent<Character>();
    }

    protected override void OnUpdate()
    {
        UpdateCeil();
        
        Vector3 moveDirection;
        if (CheckMove(out moveDirection))
        {
            Vector3 movePos = transform.position + moveDirection*_character.moveSpeed*Time.deltaTime;
            Vector2 clampPos = new Vector2(movePos.x, movePos.z);
            clampPos = _currCeilLimtBorder.Clamp(clampPos);
            movePos.x = clampPos.x;
            movePos.z = clampPos.y;
            transform.position = movePos;
            transform.forward = moveDirection;
            _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 1f);
            _animator.speed = _character.animatorSpeed;
        }
        else
        {
            _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 0f);
            _animator.speed = 1f;
        }
    }

    public override void Off()
    {
        base.Off();

        _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 0f);
        _animator.speed = 1f;
    }

    private void UpdateCeil()
    {
        if (Map.instance.GetCeil(transform.position) != _currCeil)
        {
            _currCeil = Map.instance.GetCeil(transform.position);
            UpdateBorder();
        }
    }

    private bool CheckMove(out Vector3 direction)
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(v) > 0.3f || Mathf.Abs(h) > 0.3f)
        {
            direction = new Vector3(h, 0f, v);
            return true;
        }
        direction = Vector3.zero;
        return false;
    }

    private void UpdateBorder()
    {        
        CeilCoordinate coordinate = Map.instance.GetCeil(transform.position).coordinate;
        Vector3 centerPos = Map.instance.GetCeil(transform.position).centerPosition;
        float ceilRadius = 0.5f;
        float offset = ceilRadius - _coillideRadius;

        Vector2 minPos = new Vector2(centerPos.x - ceilRadius, centerPos.z - ceilRadius);
        Vector2 maxPos = new Vector2(centerPos.x + ceilRadius, centerPos.z + ceilRadius);

        _currCeilLimtBorder = new Border
        {
            min = minPos,
            max = maxPos,
            leftEdgeIsOpen = true,
            rightEdgeIsOpen = true,
            upEdgeIsOpen = true,
            downEdgeIsOpen = true
        };

        if (Map.instance.GetCeil(coordinate.row, coordinate.col - 1) != null &&
            IsObstacle(Map.instance.GetCeil(coordinate.row, coordinate.col - 1)))
        {
            minPos.x = centerPos.x - offset;
            _currCeilLimtBorder.leftEdgeIsOpen = false;
        }

        if (Map.instance.GetCeil(coordinate.row, coordinate.col + 1) != null &&
            IsObstacle(Map.instance.GetCeil(coordinate.row, coordinate.col + 1)))
        {
            maxPos.x = centerPos.x + offset;
            _currCeilLimtBorder.rightEdgeIsOpen = false;
        }

        if (Map.instance.GetCeil(coordinate.row - 1, coordinate.col) != null &&
            IsObstacle(Map.instance.GetCeil(coordinate.row - 1, coordinate.col)))
        {
            minPos.y = centerPos.z - offset;
            _currCeilLimtBorder.downEdgeIsOpen = false;
        }

        if (Map.instance.GetCeil(coordinate.row + 1 , coordinate.col) != null &&
            IsObstacle(Map.instance.GetCeil(coordinate.row + 1, coordinate.col)))
        {
            maxPos.y = centerPos.z + offset;
            _currCeilLimtBorder.upEdgeIsOpen = false;
        }

        _currCeilLimtBorder.min = minPos;
        _currCeilLimtBorder.max = maxPos;
    }

    private bool IsObstacle(Ceil ceil)
    {
        return ceil.items.Any(ceilItem => _obstacleTags.Contains(ceilItem.tag));
    }

    private void OnDrawGizmos()
    {
        if (_currCeilLimtBorder != null)
        {
            Gizmos.color = _currCeilLimtBorder.leftEdgeIsOpen ? Color.red : Color.yellow;
            Gizmos.DrawLine(new Vector3(_currCeilLimtBorder.min.x, 3f, _currCeilLimtBorder.min.y),
                new Vector3(_currCeilLimtBorder.min.x, 3f, _currCeilLimtBorder.max.y));

            Gizmos.color = _currCeilLimtBorder.rightEdgeIsOpen ? Color.red : Color.yellow;
            Gizmos.DrawLine(new Vector3(_currCeilLimtBorder.max.x, 3f, _currCeilLimtBorder.min.y),
                new Vector3(_currCeilLimtBorder.max.x, 3f, _currCeilLimtBorder.max.y));

            Gizmos.color = _currCeilLimtBorder.upEdgeIsOpen ? Color.red : Color.yellow;
            Gizmos.DrawLine(new Vector3(_currCeilLimtBorder.min.x, 3f, _currCeilLimtBorder.max.y),
                new Vector3(_currCeilLimtBorder.max.x, 3f, _currCeilLimtBorder.max.y));

            Gizmos.color = _currCeilLimtBorder.downEdgeIsOpen ? Color.red : Color.yellow;
            Gizmos.DrawLine(new Vector3(_currCeilLimtBorder.min.x, 3f, _currCeilLimtBorder.min.y),
                new Vector3(_currCeilLimtBorder.max.x, 3f, _currCeilLimtBorder.min.y));
        }
    }
}
