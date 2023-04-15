using System.Collections;
using UnityEngine;
//A script for the AoeOverTime Spell
public class ElementalAoeOverTime : MonoBehaviour
{
    ParticleSystem ps;
    public int damage;
    public float duration;
    public ParticleSystem[] ElementalParticles;
    public Element element;
    float damageDealingInterval = 0.25f;
    float intervalCooldown;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule elementalParticles = ps.main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
        ps.Play();
    }
    //The Aoe Damage coroutine is being sarted once every 0.25 second.
    void FixedUpdate()
    {
        if (intervalCooldown > 0)
        {
            intervalCooldown -= Time.deltaTime;
        }
        else
        {
            StartCoroutine("aoeDamage");
            intervalCooldown = damageDealingInterval;
        }
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
        duration -= Time.deltaTime;

    }
    //Coroutine that turns the collider on and of.
    IEnumerator aoeDamage()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        GetComponent<CircleCollider2D>().enabled = false;
        yield return null;
    }
    //Deals damage to enemies in the range of the spell when the collider is active.
    private void OnTriggerStay2D(Collider2D collision)
    {
        string collisionObjectTag = collision.tag;
        switch (collisionObjectTag)
        {
            case "enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(damage * GameControler.getElementalMultiplier(element, collision.gameObject.GetComponent<Enemy>().gene.element)));
                break;
        }

    }
}
