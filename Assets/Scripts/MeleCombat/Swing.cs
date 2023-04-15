using UnityEngine;
//A swing class, responsible for checking collisions with enemies and objects.
public class Swing : MonoBehaviour
{
    public int swingDamage;
    public float swingSpeed;
    public Element swingElement;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1 / swingSpeed);
    }
    //Collision checks for the swiing.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionObjectTag = collision.gameObject.tag;
        switch (collisionObjectTag)
        {
            case "enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(swingDamage * GameControler.getElementalMultiplier(swingElement, collision.gameObject.GetComponent<Enemy>().gene.element)));
                if (collision.gameObject.GetComponent<Enemy>().behaviours.Contains("thorns"))
                {
                    int tempDmg = (int)(swingDamage * 0.1f);//0.1f current portion of damage reflected to the player from thorns.
                    player.GetComponent<Player>().TakeDamage(tempDmg);
                    collision.gameObject.GetComponent<Enemy>().gene.damageDealt += tempDmg;
                }
                break;
            case "Destructable":
                collision.gameObject.GetComponent<Destructables>().DestroyHandler();
                break;
        }


    }
}
