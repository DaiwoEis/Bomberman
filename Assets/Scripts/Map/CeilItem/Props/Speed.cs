using UnityEngine;

public class Speed : Props 
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<Character>().speedLevel += 1;
    }
}
