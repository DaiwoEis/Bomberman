using UnityEngine;

public class Bomb : MonoBehaviour
{
    public BombBag bag { get; set; }

    public GameObject placer { get; set; }

    public int power { get; set; }

    private bool _exploded = false;

    [SerializeField]
    private AudioClip _chargedSound = null;

    [SerializeField]
    private AudioClip _explosionSound = null;

    [SerializeField]
    private GameObject _displayItem = null;

    [SerializeField]
    private GameObject _explosionManager = null;

    private AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Invoke("Explode", 3f);
        _audioSource.clip = _chargedSound;
        _audioSource.Play();
    }

    private void Explode()
    {
        _exploded = true;

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        GameObject explosionManager = Instantiate(_explosionManager, transform.position, Quaternion.identity);
        explosionManager.GetComponent<ExplosionManager>().power = power;

        _displayItem.SetActive(false);
        GetComponent<Collider>().enabled = false;

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
        if (!_exploded && other.CompareTag("ExplosionFlame"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }
}