using UnityEngine;

public abstract class Props : Actor, IHitable
{
    [SerializeField]
    protected AudioClip _powerUpSound = null;

    [SerializeField]
    protected GameObject _showModel = null;

    protected AudioSource _audioSource = null;

    private bool _isHide = true;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        _showModel.SetActive(true);
        TriggerOnSpawnEvent();
    }

    protected virtual void Update()
    {
        _isHide = Map.instance.GetCeil(transform.position).items.Count > 1;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_isHide) return;

        if (other.CompareTag(TagConfig.PLAYER))
        {
            ApplyEffect(other.gameObject);

            _audioSource.clip = _powerUpSound;
            _audioSource.Play();

            _showModel.SetActive(false);

            TriggerOnDeathEvent();

            Destroy(gameObject, _powerUpSound.length + 0.05f);
        }
    }

    public abstract void ApplyEffect(GameObject player);

    public bool CanHit(GameObject hitter)
    {
        return hitter.CompareTag(TagConfig.EXPLOSION_FLAME) && !_isHide;
    }

    public virtual void Hit(GameObject hitter)
    {
        TriggerOnDeathEvent();
        _showModel.SetActive(false);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        TriggerOnDestroyEvent();
    }
}
