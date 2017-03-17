using UnityEngine;

public abstract class Props : Actor , IHitable 
{
    [SerializeField]
    protected AudioClip _powerUpSound = null;

    [SerializeField]
    protected GameObject _showModel = null;

    protected AudioSource _audioSource = null;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void OnEnable()
    {
        _showModel.SetActive(true);
        TriggerOnShowEvent();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);

            _audioSource.clip = _powerUpSound;
            _audioSource.Play();

            _showModel.SetActive(false);

            TriggerOnHideEvent();

            Destroy(gameObject, _powerUpSound.length + 0.05f);
        }
    }

    public abstract void ApplyEffect(GameObject player);

    public bool CanHit(GameObject hitter)
    {
        return hitter.CompareTag("ExplosionFlame");
    }

    public virtual void Hit(GameObject hitter)
    {
        TriggerOnHideEvent();
        _showModel.SetActive(false);

        Destroy(gameObject);
    }
}
