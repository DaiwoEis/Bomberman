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
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*0.5f, Screen.height*0.5f, _distance));
        Vector3 direction = _target.position - pos;
        direction.y = 0f;
        direction.Normalize();
        transform.position += direction*_moveSpeed*Time.deltaTime;
    }
}
