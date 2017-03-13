using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Role _role = null;

    [SerializeField]
    private Rigidbody _rigidbody = null;

    [SerializeField]
    private Animator _animator = null;

    private void Awake()
    {
        _role = GetComponent<Role>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() { }

    private void Update()
    {
        Vector3 moveDirection;
        if (CheckMove(out moveDirection))
        {
            _rigidbody.velocity = moveDirection*_role.moveSpeed;
            transform.forward = moveDirection;
        }

        _animator.SetFloat("Speed", _rigidbody.velocity.magnitude/_role.moveSpeed);
        _animator.speed = _role.animatorSpeed;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
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

    private void OnCollisionEnter(Collision other)
    {
        IHitable hitTarget = other.collider.GetComponent<IHitable>();
        if (hitTarget != null && hitTarget.CanHit(gameObject))
        {
            hitTarget.Hit();
        }
    }
}
