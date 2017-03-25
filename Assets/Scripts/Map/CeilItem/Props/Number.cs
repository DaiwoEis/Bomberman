using UnityEngine;

public class Number : Props 
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<Character>().bombNumberLevel += 1;
    }
}
