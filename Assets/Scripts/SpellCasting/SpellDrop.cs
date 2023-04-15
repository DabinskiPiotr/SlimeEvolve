using UnityEngine;
//SpellDrop class responsible for handling spells dropped from a chest, laying on the ground containing spell properties.
public class SpellDrop : MonoBehaviour
{
    public SpellInitializer spellInitializer;
    public Sprite[] elements;
    public SpriteRenderer elementIcon;
    public Color color;
    bool inRange;
    public SpriteRenderer e;

    private void Start()
    {//initializing visuals and collor of the spell drop.
        GetComponent<SpriteRenderer>().sprite = spellInitializer.spellSprite;
        ColorUtility.TryParseHtmlString(GameControler.colors[Elements.elements.IndexOf(spellInitializer.element)], out color);
        GetComponent<SpriteRenderer>().color = color;
        elementIcon.sprite = elements[Elements.elements.IndexOf(spellInitializer.element)];
    }
    //Player can pick up a spell by being in range of it and pressing E button.
    private void Update()
    {
        e.enabled = inRange;
        if (Input.GetKey(KeyCode.E) && inRange)
        {
            HoldSpell.spellObject.GetComponent<HoldSpell>().ChangeSpell(spellInitializer, color);
            SpellCast.spellCast.GetComponent<SpellCast>().UpdateSpellProperties();
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

}

