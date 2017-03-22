using UnityEngine;

public class CeilItem : MonoBehaviour
{
    [SerializeField]
    private string _shorterName = "";

    public string shorterName { get { return _shorterName; } }

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