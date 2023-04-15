using UnityEngine;
using UnityEngine.UI;
//A script responsible for display and change of the HUD.
public class CooldownIndicators : MonoBehaviour
{
    public GameObject[] onCooldowns;
    public GameObject spellIcon;
    public Text castCounter;
    public static GameObject cooldownIndicators;
    void Start()
    {
        cooldownIndicators = gameObject;
        spellIcon.GetComponent<Image>().sprite = HoldSpell.spellObject.GetComponent<HoldSpell>().getSpellSprite();
        spellIcon.GetComponent<Image>().color = HoldSpell.spellObject.GetComponent<HoldSpell>().getSpellColor();
        updateSpellCastCounter(SpellCast.castCounter);
    }

    //All functions for making the inactive abilities grey.
    public void BasicAttackOnCooldown(bool b)
    {
        onCooldowns[0].GetComponent<Image>().enabled = b;
    }
    public void DashOnCooldown(bool b)
    {
        onCooldowns[1].GetComponent<Image>().enabled = b;
    }
    public void BlinkOnCooldown(bool b)
    {
        onCooldowns[2].GetComponent<Image>().enabled = b;
    }
    public void SpellOnCooldown(bool b)
    {
        onCooldowns[3].GetComponent<Image>().enabled = b;
    }
    //Function for changing the current spell sprite.
    public void updateSpellIcon(Sprite sprite)
    {
        spellIcon.GetComponent<Image>().sprite = sprite;
    }
    //Function for changing the current spell color.
    public void updateSpellIconColor(Color color)
    {
        spellIcon.GetComponent<Image>().color = color;
    }
    //Function for updating the current spell cast counter.
    public void updateSpellCastCounter(int i)
    {
        castCounter.GetComponent<Text>().text = i.ToString();
    }

}
