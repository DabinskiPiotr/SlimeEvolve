using UnityEngine;
//A class that handles trigger of the melee attack objects.
public class EnemyMeleeAttack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int tempDmg = (int)(GetComponentInParent<Enemy>().gene.attack * GameControler.getElementalMultiplier(GetComponentInParent<Enemy>().gene.element, collision.gameObject.GetComponent<Player>().element));
            collision.GetComponent<Player>().TakeDamage(tempDmg);
            GetComponentInParent<Enemy>().gene.damageDealt += tempDmg;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
