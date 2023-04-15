using UnityEngine;
//A class that handles the bullet instantiated by the ranged attacking enemies.
public class EnemyBullet : MonoBehaviour
{
    public Enemy enemyParent;
    public Element bulletElement;
    Color color;
    public float bulletSpeed;
    public int bulletDamage;
    Vector2 startingScale;
    Rigidbody2D rb;
    void Start()
    {
        //Initializing color, speed and direction of the bullet.
        ColorUtility.TryParseHtmlString(GameControler.colors[Elements.elements.IndexOf(bulletElement)], out color);
        GetComponent<SpriteRenderer>().color = color;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (Player.playerTransform.position - transform.position).normalized * bulletSpeed;
        startingScale = transform.localScale;
    }

    void Update()
    {//Increasing the bulet size slightly during it's lifetime.
        if (transform.localScale.x < startingScale.x * 2.3)
        {
            transform.localScale += new Vector3(0.002f, 0.002f, 0f);
        }
    }
    //Function that handles events that hapen on collision with different objects. Bullet destroys destructables and deals dmaage to the player.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionObjectTag = collision.tag;
        switch (collisionObjectTag)
        {
            case "Player":
                int tempDmg = (int)(bulletDamage * GameControler.getElementalMultiplier(bulletElement, collision.gameObject.GetComponent<Player>().element));
                collision.GetComponent<Player>().TakeDamage(tempDmg);
                if (enemyParent != null)
                {
                    enemyParent.gameObject.GetComponent<Enemy>().gene.damageDealt += tempDmg;
                }
                Destroy(gameObject);
                break;
            case "Destructable":
                collision.gameObject.GetComponent<Destructables>().DestroyHandler();
                Destroy(gameObject);
                break;
        }
    }
}
