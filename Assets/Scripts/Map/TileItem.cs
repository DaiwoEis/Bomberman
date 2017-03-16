using UnityEngine;

public class TileItem : MonoBehaviour
{
    [SerializeField]
    private string _name = "";

    public new string name { get { return _name; } }

    [SerializeField]
    private TileItemType _type = default(TileItemType);

    public TileItemType type { get { return _type; } }
}

public enum TileItemType
{
    Creature,
    Props,
    Wall,
    Bomb
}