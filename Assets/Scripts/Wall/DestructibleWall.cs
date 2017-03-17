using UnityEngine;

public class DestructibleWall : Actor, IHitable
{
    [SerializeField]
    private bool _contaionProps = false;

    [SerializeField]
    private GameObject _propsPrefab = null;

    [SerializeField]
    private GameObject _showModel = null;

    [SerializeField]
    private float _destroyTimeAfterDamaged = 0.5f;

    private bool _hitted = false;

    private void Start()
    {
    }

    private void OnEnable()
    {
        _hitted = false;
        _showModel.SetActive(true);
        TriggerOnShowEvent();
    }

    public bool CanHit(GameObject hitter)
    {
        return hitter.CompareTag("ExplosionFlame") && _hitted == false;
    }

    public void Hit(GameObject hitter)
    {
        _hitted = true;
        _showModel.SetActive(false);
        TriggerOnHideEvent();
        Destroy(gameObject, _destroyTimeAfterDamaged);
        if (_contaionProps)
            Invoke("SpawnProps", _destroyTimeAfterDamaged - 0.1f);
    }

    private void SpawnProps()
    {
        Instantiate(_propsPrefab, transform.position, Quaternion.identity);
    }
}
