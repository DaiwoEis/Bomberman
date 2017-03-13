using System.Collections;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab = null;

    [SerializeField]
    private float _destroyTime = 3f;

    public int power { get; set; }

	private void Start ()
	{
	    Explosion();
        Destroy(gameObject, _destroyTime);
	}	

    public void Explosion()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i <= power; i++)
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction, out hitInfo, i) &&
                hitInfo.collider.CompareTag("Block"))
            {
                break;
            }

            Instantiate(_explosionPrefab, transform.position + i * direction, _explosionPrefab.transform.rotation);
            yield return new WaitForSeconds(.05f);
        }
    }
}
