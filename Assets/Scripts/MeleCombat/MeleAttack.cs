using UnityEngine;
//A script that handles singing with a weapon.
public class MeleAttack : MonoBehaviour
{

    float swingSpeed;
    int swingDamage;
    Element element;
    public GameObject swingEffect;
    public GameObject spellCast;

    GameObject handHolder;
    public GameObject attackDirection;
    Vector3 target;
    float swingAngle;
    public static bool swinging;
    public static GameObject meleAttack;
    int swing = 1;
    int critChance;
    float y;
    float z;

    void Start()
    {
        handHolder = transform.parent.gameObject;
        meleAttack = gameObject;
        UpdateWeaponProperties();
    }

    void Update()
    {
        Swing();
        weaponHoldFix();
    }
    //Function that fixes the rotation of one sided weapons so they look correctly in between swings.
    void weaponHoldFix()
    {
        z = swing == 1 ? 0 : 180;
        if (swinging == false)
        {
            y = Mathf.Lerp(y, z, Time.deltaTime * swingSpeed);
            transform.localRotation = Quaternion.Euler(0, y, z);
        }
    }
    //Function for updating properties and statistics of a weapon.
    public void UpdateWeaponProperties()
    {
        swingEffect = GetComponentInChildren<Weapon>().weaponSwingType;
        swingDamage = GetComponentInChildren<Weapon>().weaponAttackDamage;
        swingSpeed = GetComponentInChildren<Weapon>().weaponSwingSpeed;
        critChance = GetComponentInChildren<Weapon>().weaponCritChance;
        element = GetComponentInChildren<Weapon>().weaponElement;
    }
    //A swing function, it is responsible for altering between up and down swings and changing the rotation of hand and a hand anchor to imitate the swing movement. 
    void Swing()
    {
        Vector3 rotation = handHolder.transform.eulerAngles;
        swingAngle = Mathf.Lerp(swingAngle, swing * 90, Time.deltaTime * swingSpeed);
        rotation.z = Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg + swingAngle;
        handHolder.transform.eulerAngles = rotation;
        float t = swing == 1 ? 45 : -225;
        target.z = Mathf.Lerp(target.z, t, Time.deltaTime * swingSpeed);
        if (Mathf.Abs(t - target.z) < 5)
        {
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().BasicAttackOnCooldown(false);
            swinging = false;
        }
        transform.localRotation = Quaternion.Euler(target);

        if (Input.GetKey(KeyCode.Mouse0) && !swinging)
        {
            swing *= -1;
            //Instantiating the swing.
            Invoke("SwingEffect", 1/swingSpeed);
            //If the elemental swing spell is on it calls the effect as well, scaling it with the swing size of current weapon.
            spellCast.GetComponent<SpellCast>().CastElementalSwingOnAttack(swingEffect);
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().BasicAttackOnCooldown(true);
            swinging = true;
        }
    }
    //Function to calculate critical hits and if it hapens base on the critical strike chance.
    int Crit(int critChance)
    {
        if (Random.Range(1, 101) >= critChance)
        {
            return 2;
        }
        return 1;
    }
    //A function that instantiates the swing and passes adequate variables to it.
    void SwingEffect()
    {
        GameObject tempSwingEffect = Instantiate(swingEffect, attackDirection.transform.GetChild(0).position, attackDirection.transform.rotation);
        Swing swing = tempSwingEffect.GetComponent<Swing>();
        swing.swingDamage = (transform.root.gameObject.GetComponent<Player>().attackDamage + swingDamage) * Crit(critChance);
        swing.swingSpeed = swingSpeed;
        swing.swingElement = element;
        swing.player = transform.root;
    }
}
