using UnityEngine;
using UnityEngine.UI;
//The script responsible for updating the UI health of a player.
public class PlayerHealthBar : MonoBehaviour
{
    Slider slider;
    int health;
    int maxHealth;

    void Start()
    {
        slider = GetComponent<Slider>();
        maxHealth = Player.maxHealth;
    }
    void Update()
    {
        health = Player.health;
        slider.value = (float)health / maxHealth;
        slider.GetComponentInChildren<Text>().text = health.ToString() + "/" + maxHealth.ToString();
    }
}
