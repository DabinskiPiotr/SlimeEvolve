using System.Collections.Generic;

//A gene class. Generally a container for variables that evolution class can operate on.
public class Gene
{
    public int id;
    public int attack;
    public int maxHealth;
    public float speed;
    public float attackSpeed;
    public float attackCooldown;
    public int bulletSpeed;
    public float attackRange;
    public float visionRange;
    public List<string> behaviours;
    public Element element;
    public int damageDealt;
    //The gene constructor.
    public Gene(
        int id,
        int attack,
        int maxHealth,
        float speed,
        float attackSpeed,
        float attackCooldown,
        int bulletSpeed,
        float attackRange,
        float visionRange,
        List<string> behaviours,
        Element element,
        int damageDealt
        )
    {
        this.id = id;
        this.attack = attack;
        this.maxHealth = maxHealth;
        this.speed = speed;
        this.attackSpeed = attackSpeed;
        this.attackCooldown = attackCooldown;
        this.bulletSpeed = bulletSpeed;
        this.attackRange = attackRange;
        this.visionRange = visionRange;
        this.behaviours = behaviours;
        this.element = element;
        this.damageDealt = damageDealt;
    }
    //An overriden toString function for gene class.
    public override string ToString()
    {
        string behaviourList = "";
        foreach (string b in behaviours)
        {
            behaviourList += b + "/";
        }
        return string.Format("{0}/ {1}/ {2}/ {3}/ {4}/ {5}/ {6}/ {7}/ {8}/ ::{9}::/ {10}/ {11}",
            id,
            attack,
            maxHealth,
            speed,
            attackSpeed,
            attackCooldown,
            bulletSpeed,
            attackRange,
            visionRange,
            behaviourList,
            element,
            damageDealt
            );
    }
}
