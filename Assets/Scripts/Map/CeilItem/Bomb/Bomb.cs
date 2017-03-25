using UnityEngine;

public class Bomb : Actor
{
    public BombBag bag { get; set; }

    public GameObject placer { get; set; }

    public int power { get; set; }

    private bool _exploded = false;

    [SerializeField]
    private float _chargedTime = 3f;

    [SerializeField]
    private AudioClip _chargedSound = null;

    [SerializeField]
    private AudioClip _explosionSound = null;

    [SerializeField]
    private GameObject _showItem = null;

    [SerializeField]
    private GameObject _explosionManager = null;

    private AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        TriggerOnSpawnEvent();

        Invoke("Explode", _chargedTime);

        _audioSource.clip = _chargedSound;
        _audioSource.Play();
    }

    private void Explode()
    {
        _exploded = true;

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        GameObject explosionManager = Instantiate(_explosionManager, transform.position, Quaternion.identity);
        explosionManager.GetComponent<ExplosionController>().power = power;

        _showItem.SetActive(false);
        GetComponent<Collider>().enabled = false;
        TriggerOnDeathEvent();

        bag.ReturnBag();

        Destroy(gameObject, _explosionSound.length + 0.2f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == placer)
        {
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_exploded && other.CompareTag(TagConfig.EXPLOSION_FLAME))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }

    private void OnDestroy()
    {
        TriggerOnDestroyEvent();
    }
}