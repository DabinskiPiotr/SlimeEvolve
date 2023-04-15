using UnityEngine;
//An Elemental ball spell script.
public class ElementalBall : MonoBehaviour
{
    Color color;
    public Rigidbody2D rb;
    public Element spellElement;
    public ParticleSystem[] ElementalParticles;
    public int spellDamage;
    void Start()
    {//Setting the bullet visuals, damage and speed.
        ColorUtility.TryParseHtmlString(GameControler.colors[Elements.elements.IndexOf(spellElement)], out color);
        GetComponent<SpriteRenderer>().color = color;
        ParticleSystem.MainModule elementalParticles = GetComponent<ParticleSystem>().main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(spellElement)].main.startColor.gradient);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (AttackDirection.direction - transform.position).normalized * 10;
        Destroy(gameObject, 1f);
    }
    //Destroying the destructables and dealing daage to enemies on contatct. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionObjectTag = collision.tag;
        switch (collisionObjectTag)
        {
            case "enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(spellDamage * GameControler.getElementalMultiplier(spellElement, collision.gameObject.GetComponent<Enemy>().gene.element)));
                Destroy(gameObject);
                break;
            case "Destructable":
                collision.gameObject.GetComponent<Destructables>().DestroyHandler();
                break;
        }
    }
}
