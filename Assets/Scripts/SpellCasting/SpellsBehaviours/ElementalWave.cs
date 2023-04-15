using System.Collections;
using UnityEngine;
//Script responsible for the Elemental Wave spell.
public class ElementalWave : MonoBehaviour
{
    Color color;
    const int small = 0;
    const int medium = 1;
    const int big = 2;
    public GameObject[] children;
    public Element element;
    public int damage;
    public ParticleSystem[] ElementalParticles;

    private void Start()
    {
        ColorUtility.TryParseHtmlString(GameControler.colors[Elements.elements.IndexOf(element)], out color);
        children[small].GetComponent<SpriteRenderer>().color = color;
        children[medium].GetComponent<SpriteRenderer>().color = color;
        children[big].GetComponent<SpriteRenderer>().color = color;
        ParticleSystem.MainModule elementalParticles = children[small].GetComponent<ParticleSystem>().main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
        elementalParticles = children[medium].GetComponent<ParticleSystem>().main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
        elementalParticles = children[big].GetComponent<ParticleSystem>().main;
        elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
        StartCoroutine("WaveCor");
    }
    void SwitchComponents(GameObject gameObject, bool b)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = b;
        gameObject.GetComponent<Collider2D>().enabled = b;
    }
    //Turning on and off consecutive objects with their colliders and playing their particle effects.
    IEnumerator WaveCor()
    {
        SwitchComponents(children[small], true);
        children[small].gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.1f);
        SwitchComponents(children[small], false);
        SwitchComponents(children[medium], true);
        children[medium].gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.1f);
        SwitchComponents(children[medium], false);
        SwitchComponents(children[big], true);
        children[big].gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.1f);
        SwitchComponents(children[big], false);
        Destroy(gameObject, 0.2f);
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {//Dealing damage to the enemy on collision.
        string collisionObjectTag = collision.tag;
        switch (collisionObjectTag)
        {
            case "enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(damage * GameControler.getElementalMultiplier(element, collision.gameObject.GetComponent<Enemy>().gene.element)));
                break;
        }
    }
}
