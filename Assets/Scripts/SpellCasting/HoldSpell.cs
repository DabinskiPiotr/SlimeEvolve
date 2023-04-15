using UnityEngine;
//A script that stores informations about currently held spell.
public class HoldSpell : MonoBehaviour
{
    public string spellTitle;
    public float cooldown;
    public float castTime;
    public int baseDamage;
    public static GameObject spellObject;
    public GameObject spell;
    public Element spellElement;
    public Sprite sprite;
    public GameObject CooldownsUI;
    public int castCounter;
    public Color color;

    private void Awake()
    {
        spellObject = gameObject;
        InitializeSpell(spellTitle, spell, baseDamage, cooldown, castTime, castCounter, spellElement);
    }

    //The spell initialize function only used at the start of the game.
    void InitializeSpell(string spellTitle, GameObject spell, int baseDamage, float cooldown, float castTime, int castCounter, Element spellElement)
    {
        if (WavesCounter.wavesCounter == 1)
        {
            this.spellTitle = spellTitle;
            this.spell = spell;
            this.baseDamage = baseDamage;
            this.cooldown = cooldown;
            this.castTime = castTime;
            this.spellElement = spellElement;
            this.castCounter = castCounter;
        }
    }
    public Sprite getSpellSprite()
    {
        return sprite;
    }
    public Color getSpellColor()
    {
        return color;
    }
    //Changes the held spell variables and properties.
    public void ChangeSpell(SpellInitializer newSpell, Color color)
    {
        spellTitle = newSpell.spellTitle;
        spell = newSpell.spell;
        baseDamage = newSpell.baseDamage;
        cooldown = newSpell.cooldown;
        castTime = newSpell.castTime;
        spellElement = newSpell.element;
        sprite = newSpell.spellSprite;
        castCounter = newSpell.castCounter;
        this.color = color;
        CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellIcon(newSpell.spellSprite);
        CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellIconColor(color);
        CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellCastCounter(newSpell.castCounter);
    }
}
