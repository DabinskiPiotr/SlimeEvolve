using UnityEngine;
//A Script that handles chest breaking and initializing a drop of a weapon from it.
public class Chest : MonoBehaviour
{
    public WeaponObjects[] weaponObjects;
    public GameObject weapon;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spell"))// A swing for this context also has a tag of "Spell".
        {
            GetComponent<BoxCollider2D>().enabled = false;
            ParticleSystem ps = GetComponent<ParticleSystem>();
            WeaponInitializer weaponInitializer = new WeaponInitializer
             (weaponObjects[Random.Range(0, weaponObjects.Length)],
             Random.Range(20, 30), //random damage between two numbers multiplied by the damage multiplier in the constructor
             Random.Range(7f, 14f), //random swing speed between two numbers multiplied by the swing speed multiplier in the constructor
             Elements.elements[Random.Range(0, 9)]//random element, including the 9th - none element for weapons.
             );
            GameObject tempWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
            tempWeapon.GetComponent<WeaponDrop>().weaponInitializer = weaponInitializer;
            ps.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, ps.main.duration);
        }
    }
}
