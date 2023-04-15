using UnityEngine;
//A Script that handles chest breaking and initializing a drop of a spell from it.
public class SpellChest : MonoBehaviour
{
    public SpellObjects[] spellObjects;
    public GameObject spellDrop;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spell"))// A swing for this context also has a tag of "Spell".
        {
            GetComponent<BoxCollider2D>().enabled = false;
            ParticleSystem ps = GetComponent<ParticleSystem>();
            SpellInitializer spellInitializer = new SpellInitializer
            (spellObjects[Random.Range(0, spellObjects.Length)],
            Elements.elements[Random.Range(0, 8)]//random element
            );
            GameObject tempSpell = Instantiate(spellDrop, transform.position, Quaternion.identity);
            tempSpell.GetComponent<SpellDrop>().spellInitializer = spellInitializer;
            ps.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, ps.main.duration);

        }
    }
}
