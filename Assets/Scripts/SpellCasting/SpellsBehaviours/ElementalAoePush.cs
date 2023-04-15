using UnityEngine;
//A script responsible for the AoePush spell
public class ElementalAoePush : MonoBehaviour
{
    public Element element;
    public int damage;
    ParticleSystem ps;
    public ParticleSystem[] ElementalParticles;

    void Start()
    {//Particle effect visuals
        ps = GetComponentInParent<ParticleSystem>();
        ParticleSystem.MainModule elementalParticles = ps.main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
        ps.Play();
    }

    void FixedUpdate()
    {//Grows the collider so it looks like the spell is pushing enemies
        if (transform.localScale.x <= 8)
        {
            transform.localScale += new Vector3(0.3f, 0.3f);
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(transform.parent.gameObject, 1);
        }
    }
    //Deals damage on contact
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionObjectTag = collision.gameObject.tag;
        switch (collisionObjectTag)
        {
            case "enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(damage * GameControler.getElementalMultiplier(element, collision.gameObject.GetComponent<Enemy>().gene.element)));
                break;
        }
    }
}
