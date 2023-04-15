using UnityEngine;
//A script attached to the held weapon.
public class Weapon : MonoBehaviour
{
    public string weaponTitle;
    public GameObject weaponSwingType;
    public Sprite weaponSprite;
    public int weaponAttackDamage;
    public float weaponSwingSpeed;
    public Element weaponElement;
    public static GameObject weapon;
    public int weaponCritChance;
    private void Start()
    {
        weapon = gameObject;
        InitializeWeapon(weaponTitle, weaponSwingType, weaponAttackDamage, weaponSwingSpeed, weaponCritChance, weaponElement);
    }

    //Initializing the starting weapon based on the variables set in the inspector.
    void InitializeWeapon(string weaponTitle, GameObject weaponSwingType, int weaponAttackDamage, float weaponSwingSpeed, int weaponCritChance, Element weaponElement)
    {
        if (WavesCounter.wavesCounter == 1)
        {
            GetComponent<SpriteRenderer>().sprite = weaponSprite;
            this.weaponTitle = weaponTitle;
            this.weaponSwingType = weaponSwingType;
            this.weaponAttackDamage = weaponAttackDamage;
            this.weaponSwingSpeed = weaponSwingSpeed;
            this.weaponCritChance = weaponCritChance;
            this.weaponElement = weaponElement;
        }
    }
    //Function that changes attributes and variables of the weapon based on the new, picked up weapon.
    public void ChangeWeapon(WeaponInitializer newWeapon, Color color)
    {
        GetComponent<SpriteRenderer>().sprite = newWeapon.weaponSprite;
        GetComponent<SpriteRenderer>().color = color;
        weaponTitle = newWeapon.weaponTitle;
        weaponSwingType = newWeapon.swingType;
        weaponAttackDamage = newWeapon.attackDamage;
        weaponSwingSpeed = newWeapon.swingSpeed;
        weaponCritChance = newWeapon.critChance;
        weaponElement = newWeapon.element;
    }
}
