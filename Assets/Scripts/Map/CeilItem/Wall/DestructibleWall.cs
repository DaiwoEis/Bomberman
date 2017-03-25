using UnityEngine;

public class DestructibleWall : Actor, IHitable
{
    [SerializeField]
    private bool _containProps = false;

    [SerializeField]
    private GameObject _spawnObjectAfterDamaged = null;

    [SerializeField]
    private GameObject _showModel = null;

    [SerializeField]
    private float _destroyTimeAfterDamaged = 0.5f;

    private bool _hitted = false;

    private void Start()
    {
        _hitted = false;
        _showModel.SetActive(true);
        TriggerOnSpawnEvent();
    }

    public bool CanHit(GameObject hitter)
    {
        return hitter.CompareTag(TagConfig.EXPLOSION_FLAME) && _hitted == false;
    }

    public void Hit(GameObject hitter)
    {
        _hitted = true;
        _showModel.SetActive(false);
                
        Destroy(gameObject, _destroyTimeAfterDamaged);
        if (_containProps)
            Invoke("SpawnProps", _destroyTimeAfterDamaged - 0.1f);
    }

    private void SpawnProps()
    {
        Instantiate(_spawnObjectAfterDamaged, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        TriggerOnDeathEvent();
        TriggerOnDestroyEvent();
    }
}
