using System.Collections;
using UnityEngine;
//A spell cast scrtipt responsible for casting different spells and handling their cooldowns and cast counters.
public class SpellCast : MonoBehaviour
{
    GameObject spell;
    public GameObject aimDirection;
    public GameObject weapon;
    public static GameObject spellCast;
    float cooldown;
    float currentSpellCooldown;
    int damage;
    public static int castCounter;
    Element element;
    string currentSpell;
    bool isElementalSwingOn = false;
    public ParticleSystem[] ElementalParticles;
    float elementalSwingDuration;

    private void Start()
    {
        spellCast = gameObject;
        UpdateSpellProperties();
    }

    private void Update()
    {
        CastSpell();
    }
    //Function responsible for casting an approperiate spell and calculating cooldowns.
    void CastSpell()
    {
        switch (currentSpell)
        {

            case "ElementalBall":
                CastElementalBall();
                break;
            case "ElementalAoePush":
                CastElementalAoePush();
                break;
            case "ElementalAoeOverTime":
                CastElementalAoeOverTime();
                break;
            case "ElementalExplosion":
                CastElementalExplosion();
                break;
            case "ElementalWave":
                CastElementalWave();
                break;
            case "ElementalSwing":
                CastElementalSwing();
                break;
        }
        if (cooldown > 0)
        {
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(true);
            cooldown -= Time.deltaTime;
        }
        else if (castCounter > 0)
        {
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(false);
        }
        if (elementalSwingDuration > 0)
        {
            elementalSwingDuration -= Time.deltaTime;
        }
        else
        {
            DisableElementalSwingEffect();
        }
    }
    //A function that turns off the elemental swing effect off.
    void DisableElementalSwingEffect()
    {
        weapon.GetComponent<ParticleSystem>().Stop();
        isElementalSwingOn = false;
    }
    //A function responsible for updading the spell properites.
    public void UpdateSpellProperties()
    {
        currentSpell = GetComponentInChildren<HoldSpell>().spellTitle;
        spell = GetComponentInChildren<HoldSpell>().spell;
        damage = GetComponentInChildren<HoldSpell>().baseDamage;
        element = GetComponentInChildren<HoldSpell>().spellElement;
        castCounter = GetComponentInChildren<HoldSpell>().castCounter;
        currentSpellCooldown = GetComponentInChildren<HoldSpell>().cooldown;
        CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(false);
        cooldown = 0;
        DisableElementalSwingEffect();
    }
    // A function responsible for casting ElementalBall spell. Instantiating a bullet and pasing approperiate variables to it.
    void CastElementalBall()
    {
        if (Input.GetKey(KeyCode.Mouse1) && cooldown <= 0 && castCounter > 0)
        {
            GameObject tempSpell = Instantiate(spell, transform.position, aimDirection.transform.rotation);
            ElementalBall elementalBall = tempSpell.GetComponent<ElementalBall>();
            elementalBall.spellElement = element;
            elementalBall.spellDamage = damage;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(true);
            castCounter--;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellCastCounter(castCounter);
            cooldown = currentSpellCooldown;
        }


    }
    // A function responsible for casting ElementalAoePush spell. Instantiating a push/explosion and pasing approperiate variables to it.
    void CastElementalAoePush()
    {
        if (Input.GetKey(KeyCode.Mouse1) && cooldown <= 0 && castCounter > 0)
        {
            GameObject tempSpell = Instantiate(spell, transform.position, aimDirection.transform.rotation);
            ElementalAoePush elementalAoePush = tempSpell.GetComponentInChildren<ElementalAoePush>();
            elementalAoePush.element = element;
            elementalAoePush.damage = damage;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(true);
            castCounter--;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellCastCounter(castCounter);
            cooldown = currentSpellCooldown;
        }
    }
    // A function responsible for casting ElementalAoeOverTime spell. Instantiating an aura and pasing approperiate variables to it.
    void CastElementalAoeOverTime()
    {
        float duration = 10;
        if (Input.GetKey(KeyCode.Mouse1) && cooldown <= 0 && castCounter > 0)
        {
            GameObject tempSpell = Instantiate(spell, transform.root.gameObject.transform);
            ElementalAoeOverTime elementalAoeOverTime = tempSpell.GetComponentInChildren<ElementalAoeOverTime>();
            elementalAoeOverTime.element = element;
            elementalAoeOverTime.damage = damage;
            elementalAoeOverTime.duration = duration;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(true);
            castCounter--;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellCastCounter(castCounter);
            cooldown = currentSpellCooldown + duration;
        }
    }
    // A function responsible for casting ElementalExplosion spell. Instantiating an explosion and pasing approperiate variables to it.
    void CastElementalExplosion()
    {
        if (Input.GetKey(KeyCode.Mouse1) && cooldown <= 0 && castCounter > 0)
        {
            GameObject tempSpell = Instantiate(spell, Vector3.ClampMagnitude((Vector3)(Player.aim), 5) + Player.playerTransform.position, Quaternion.identity);
            ElementalExplosion elementalExplosion = tempSpell.GetComponentInChildren<ElementalExplosion>();
            elementalExplosion.element = element;
            elementalExplosion.damage = damage;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(true);
            castCounter--;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellCastCounter(castCounter);
            cooldown = currentSpellCooldown;
        }
    }
    // A function responsible for casting ElementalWave spell. Instantiating object that handles consecutive explosions and pasing approperiate variables to it.
    void CastElementalWave()
    {
        if (Input.GetKey(KeyCode.Mouse1) && cooldown <= 0 && castCounter > 0)
        {
            GameObject tempSpell = Instantiate(spell, transform.position, aimDirection.transform.rotation);
            ElementalWave elementalWave = tempSpell.GetComponentInChildren<ElementalWave>();
            elementalWave.element = element;
            elementalWave.damage = damage;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(true);
            castCounter--;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellCastCounter(castCounter);
            cooldown = currentSpellCooldown;
        }
    }
    // A function responsible for casting ElementalSwing spell. Turning on the elemental swing variable and pasing visuals and damage to the elemental swing that is instantiated on swing.
    void CastElementalSwing()
    {
        float duration = 8;
        if (Input.GetKey(KeyCode.Mouse1) && cooldown <= 0 && castCounter > 0)
        {
            weapon.GetComponent<ParticleSystem>().Play();
            ParticleSystem.MainModule elementalParticles = weapon.GetComponent<ParticleSystem>().main;
            elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(element)].main.startColor.gradient);
            isElementalSwingOn = true;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().SpellOnCooldown(true);
            castCounter--;
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().updateSpellCastCounter(castCounter);
            cooldown = currentSpellCooldown + duration;
            elementalSwingDuration = duration;
        }

    }
    // A function responsible for starting the elemental swing coroutine.
    public void CastElementalSwingOnAttack(GameObject swingSize)
    {
        if (isElementalSwingOn == true && currentSpell.Equals("ElementalSwing"))
        {
            StartCoroutine("ElementalSwingEffect", swingSize);
        }
    }
    // A coroutine for spawning elemental swing object on a swing.
    IEnumerator ElementalSwingEffect(GameObject swingSize)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempSpell = Instantiate(spell, aimDirection.transform.position, aimDirection.transform.rotation);
        tempSpell.gameObject.transform.localScale = swingSize.transform.localScale * 0.7f;
        ElementalSwing elementalSwing = tempSpell.GetComponent<ElementalSwing>();
        elementalSwing.element = element;
        elementalSwing.damage = damage;
        yield return null;
    }
}
