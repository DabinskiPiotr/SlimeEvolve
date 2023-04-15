using UnityEngine;
//WeaponDrop class responsible for handling weapon dropped from a chest, laying on the ground containing weapon properties.
public class WeaponDrop : MonoBehaviour
{
    public WeaponInitializer weaponInitializer;
    public Sprite[] elements;
    public SpriteRenderer elementIcon;
    public Color color;
    string[] colors = { "#192226", "#D01716", "#283593", "#6BC300", "#5D4037", "#FBC02D", "#8DFDFF", "#FFF176", "#FFFFFF" };
    bool inRange;
    public SpriteRenderer e;
    private void Start()
    {//initializing visuals and collor of the weapon drop.
        GetComponent<SpriteRenderer>().sprite = weaponInitializer.weaponSprite;
        ColorUtility.TryParseHtmlString(colors[Elements.elements.IndexOf(weaponInitializer.element)], out color);
        GetComponent<SpriteRenderer>().color = color;
        elementIcon.sprite = elements[Elements.elements.IndexOf(weaponInitializer.element)];
    }
    //Player can pick up a weapon by being in range of it and pressing E button.
    private void Update()
    {
        e.enabled = inRange;
        if (Input.GetKey(KeyCode.E) && inRange)
        {
            Weapon.weapon.GetComponent<Weapon>().ChangeWeapon(weaponInitializer, color);
            MeleAttack.meleAttack.GetComponent<MeleAttack>().UpdateWeaponProperties();
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
