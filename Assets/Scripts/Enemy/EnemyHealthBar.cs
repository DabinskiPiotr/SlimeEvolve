using UnityEngine;
//A class that takes care of the enemy health bar.
public class EnemyHealthBar : MonoBehaviour
{
    int health;
    int maxHealth;

    void Start()
    {
        maxHealth = GetComponentInParent<Enemy>().maxHealth;
    }
    void Update()
    {
        health = GetComponentInParent<Enemy>().health;
        transform.localScale = new Vector2((float)health / maxHealth, transform.localScale.y);
    }
}
