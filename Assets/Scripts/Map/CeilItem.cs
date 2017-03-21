using UnityEngine;

public class CeilItem : MonoBehaviour
{
    public new string name { get { return gameObject.name; } }

    [SerializeField]
    private TileItemType _type = default(TileItemType);

    public TileItemType type { get { return _type; } }
}

public enum TileItemType
{
    Creature,
    Props,
    Wall,
    Bomb,
    Door
}