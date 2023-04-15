using UnityEngine;
//An ElementalExplosion spell.
public class ElementalExplosion : MonoBehaviour
{
    public Element element;
    public int damage;
    ParticleSystem ps;
    public ParticleSystem[] ElementalParticles;

    void Start()
    {//Setting the visuals.
        ps = GetComponentInParent<ParticleSystem>();
        ParticleSystem.MainModule elementalParticles = ps.main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
    //Dealing damage on collision.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionObjectTag = collision.tag;
        switch (collisionObjectTag)
        {
            case "enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(damage * GameControler.getElementalMultiplier(element, collision.GetComponent<Enemy>().gene.element)));
                break;
        }
    }
}
