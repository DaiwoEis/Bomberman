using UnityEngine;

public class Power : Props
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<Character>().bombPowerLevel += 1;
    }
}
