using UnityEngine;

public class PlayerLifeHUD : MonoBehaviour
{
    [SerializeField]
    private PlayerLifeController _playerLifeController = null;

    [SerializeField]
    private GameObject _lifeImagePrefab = null;

    private void Awake()
    {
        GameObject.FindWithTag(TagConfig.PLAYER).GetComponent<Actor>().onSpawn += UpdateLifeImage;
    }

    private void UpdateLifeImage()
    {
        foreach (Transform childTrans in GetComponentsInChildren<Transform>())
        {
            if (childTrans == transform)
                continue;

            Destroy(childTrans.gameObject);
        }

        for (int i = 0; i < _playerLifeController.playerLife; ++i)
        {
            GameObject lifeImage = Instantiate(_lifeImagePrefab);
            lifeImage.transform.SetParent(transform);
            lifeImage.transform.localPosition = Vector3.zero;
            lifeImage.transform.localScale = Vector3.one;
            lifeImage.transform.localRotation = Quaternion.identity;
        }
    }
}
