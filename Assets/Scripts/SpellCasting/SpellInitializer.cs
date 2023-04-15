using UnityEngine;
//An initializer for spells.
public class SpellInitializer
{
    public string spellTitle;
    public float cooldown;
    public float castTime;
    public int baseDamage;
    public int castCounter;
    public Element element;
    public GameObject spell;
    public Sprite spellSprite;
    //A constructor that initialize a spell using the spell scriptable object preset.
    public SpellInitializer(SpellObjects spellObjects, Element element)
    {
        spellTitle = spellObjects.spellTitle;
        cooldown = spellObjects.cooldown;
        castTime = spellObjects.castTime;
        baseDamage = spellObjects.baseDamage;
        spellSprite = spellObjects.spellSprite;
        spell = spellObjects.spell;
        castCounter = spellObjects.castCounter;
        this.element = element;
    }
    //Overriden toString function for the spell initializer.
    public override string ToString()
    {
        return string.Format("SpellTitle {0} / Damage {1} / Cooldown {2} / Element {3} / Casts {4}",
        spellTitle,
        baseDamage,
        cooldown,
        element,
        castCounter);
    }
}
