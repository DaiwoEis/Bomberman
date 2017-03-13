using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{    
    private Role _role = null;

    [SerializeField]
    private Rigidbody _rigidbody = null;
    
    [SerializeField]
    private Animator _animator = null;
    
    private FloatLevelMaper _speedMaper = null;

    private FloatLevelMaper _animatorSpeedMaper = null;

    [SerializeField]
    private TextAsset _speedLevel = null;

    [SerializeField]
    private TextAsset _animatorSpeedLevel = null;

	private void Start ()
	{
	    _role = GetComponent<Role>();
	    _rigidbody = GetComponent<Rigidbody>();

	    _speedMaper = new FloatLevelMaper(_speedLevel);
	    _animatorSpeedMaper = new FloatLevelMaper(_animatorSpeedLevel);
	}
	
	private void Update ()
	{
	    Vector3 moveDirection;
	    if (CheckMove(out moveDirection))
	    {
	        _rigidbody.velocity = moveDirection*_speedMaper.GetData(_role.speedLevel);
	        transform.forward = moveDirection;
	    }

	    _animator.SetFloat("Speed", _rigidbody.velocity.magnitude/_speedMaper.GetData(_role.speedLevel));
	    _animator.speed = _animatorSpeedMaper.GetData(_role.speedLevel);
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
}
