using UnityEngine;
//An initializer for weapons.
public class WeaponInitializer
{
    public string weaponTitle;
    public GameObject swingType;
    public Sprite weaponSprite;
    public int attackDamage;
    public float swingSpeed;
    public int critChance;
    public Element element;
    //A constructor that initialize a weapon using the weapon scriptable object preset.
    public WeaponInitializer(WeaponObjects weaponObject, int attackDamage, float swingSpeed, Element element)
    {
        weaponTitle = weaponObject.weaponTitle;
        swingType = weaponObject.swingType;
        weaponSprite = weaponObject.weaponSprite;
        critChance = weaponObject.critChance;
        this.attackDamage = (int)(attackDamage * weaponObject.attackDamageMultiplier);
        this.swingSpeed = swingSpeed * weaponObject.swingSpeedMultiplier;
        this.element = element;
    }
    //Overriden toString function for the weapon initializer.
    public override string ToString()
    {
        return string.Format("WeaponTitle {0} / AttackDamage {1} / SwingSpeed {2} / Element {3}",
        weaponTitle,
        attackDamage,
        swingSpeed,
        element);
    }
}
