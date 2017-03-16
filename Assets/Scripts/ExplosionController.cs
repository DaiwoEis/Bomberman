using System.Collections;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab = null;

    [SerializeField]
    private LayerMask _blockLayer = default(LayerMask);

    public int power { get; set; }

    private void OnEnable()
    {
        Invoke("Explosion", 0.05f);
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

            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction, out hitInfo, i, _blockLayer))
            {
                break;
            }

            Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0),direction*i,Color.red,2f);
            Instantiate(_explosionPrefab, transform.position + i * direction, _explosionPrefab.transform.rotation);
            yield return new WaitForSeconds(.05f);
        }
    }
}
