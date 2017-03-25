using UnityEngine;

public interface IHitable
{
    bool CanHit(GameObject hitter);

    void Hit(GameObject hitter);
}
