using UnityEngine;
//The script, responsible for the Elemental swing spell.
public class ElementalSwing : MonoBehaviour
{
    public Element element;
    public int damage;
    public ParticleSystem[] ElementalParticles;
    ParticleSystem ps;

    void Start()
    {//Setting the Visuals.
        ps = GetComponentInParent<ParticleSystem>();
        ParticleSystem.MainModule elementalParticles = ps.main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {//dealing damage on collision.
        string collisionObjectTag = collision.tag;
        switch (collisionObjectTag)
        {
            case "enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(damage * GameControler.getElementalMultiplier(element, collision.GetComponent<Enemy>().gene.element)));
                break;
        }
    }
}
