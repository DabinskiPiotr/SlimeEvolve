using UnityEngine;
//A scriptable object for spells.
[CreateAssetMenu]
public class SpellObjects : ScriptableObject
{
    public string spellTitle;
    public float cooldown;
    public float castTime;
    public int baseDamage;
    public GameObject spell;
    public Sprite spellSprite;
    public int castCounter;
}
