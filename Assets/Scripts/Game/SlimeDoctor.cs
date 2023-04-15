using UnityEngine;
//A script responsible for the slime doctor on click effect.
public class SlimeDoctor : MonoBehaviour
{
    int HealsNumber;
    int HealAmount = 60;
    private void Start()
    {
        HealsNumber = 1;
    }
    //Heals and increases the players maximum healt by 15.
    private void OnMouseDown()
    {
        if (HealsNumber > 0)
        {   
            Player.player.GetComponent<Player>().Heal(HealAmount);
            Player.maxHealth += 15;
            HealsNumber--;
        }
    }
}
