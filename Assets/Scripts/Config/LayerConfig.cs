using UnityEngine;

public class LayerConfig
{
    public static readonly int PLAYER = LayerMask.NameToLayer("Player");

    public static readonly int ENEMY = LayerMask.NameToLayer("Enemy");

    public static readonly int WALL = LayerMask.NameToLayer("Wall");

    public static readonly int DESTRUCTIBLE_WALL = LayerMask.NameToLayer("DestructibleWall");
}
