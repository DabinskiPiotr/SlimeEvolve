using UnityEngine;
//A scriptable object for weapons.
[CreateAssetMenu]
public class WeaponObjects : ScriptableObject
{
    public string weaponTitle;
    public GameObject swingType;
    public Sprite weaponSprite;
    public float attackDamageMultiplier;
    public float swingSpeedMultiplier;
    public int critChance;
}
