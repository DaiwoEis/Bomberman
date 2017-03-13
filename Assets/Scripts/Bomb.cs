using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public BombBag bag { get; set; }

    public GameObject placer { get; set; }

    private bool _exploded = false;

    [SerializeField]
    private GameObject _explosionPrefab = null;

    [SerializeField]
    private AudioClip _burnSound = null;

    [SerializeField]
    private AudioClip _explosionSound = null;

    [SerializeField]
    private LayerMask _levelMask = default(LayerMask);

    [SerializeField]
    private GameObject _model = null;

    private AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Invoke("Explode", 3f);
        _audioSource.clip = _burnSound;
        _audioSource.Play();
    }

    private void Explode()
    {
        _exploded = true;

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));

        _model.SetActive(false);
        GetComponent<Collider>().enabled = false;


        bag.ReturnBag();

        Destroy(gameObject, 1f); 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == placer)
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_exploded && other.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit; 

            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, _levelMask);              

            if (!hit.collider)
            {
                Instantiate(_explosionPrefab, transform.position + i*direction, _explosionPrefab.transform.rotation);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(.05f);
        }
    }
}