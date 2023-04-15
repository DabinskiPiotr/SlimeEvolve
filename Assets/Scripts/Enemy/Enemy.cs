using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The class that handles apperance and behaviours of enemies
public class Enemy : MonoBehaviour
{
    const int chargeIndicator = 0;
    const int attackRangeIndicator = 1;
    const int elementType = 2;
    const int chargeRangeIndicator = 3;
    const int flyingIndicator = 4;
    const int attackType = 5;
    const int movementType = 6;
    const int toxicIndicator = 7;
    const int dashIndicator = 8;
    const int thornsIndicator = 9;

    const int melee = 0;
    const int ranged = 1;
    const int charge = 2;
    const int walking = 3;
    const int flying = 4;
    const int dash = 5;

    /* elements
     const int dark = 0;
     const int fire = 1;
     const int water = 2;
     const int air = 3;
     const int rock = 4;
     const int electric = 5;
     const int ice = 6;
     const int light = 7;
     const int none = 8;
    */


    Rigidbody2D rb;
    public ParticleSystem ps;
    public ParticleSystem dashPs;
    public ParticleSystem toxicPs;
    public RuntimeAnimatorController[] animatorControllers;

    public GameObject[] visuals;
    public GameObject bullet;
    public GameObject firePoint;
    public Sprite[] elements;
    public Sprite[] behavioursIcons;
    public Sprite[] slimes;
    public ParticleSystem[] ElementalParticles;

    string state = "idle";
    float attackCooldownVariable;
    bool idleMoving = false;
    public bool isDashing = false;
    bool isChasing = false;
    public int damageDealt;
    float directionX = 0;

    //statistics
    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int attack;
    float visionRange;
    float attackRange;
    float attackSpeed;
    int bulletSpeed;
    float attackCooldown;
    float speed;
    public List<string> behaviours;
    Element element;

    //additionalStatistics
    float chargeRange;
    float rangeAttackRange;

    public Gene gene;
    void Start()
    {
        damageDealt = 0;
        EnemiesCounter.enemiesCounter++;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Behaviour();
        UpdateCounters();
        Direction();
    }
    //Function that is responsible for updating all counters, currently only cooldown of the attack.
    void UpdateCounters()
    {
        if (attackCooldownVariable > 0)
        {
            attackCooldownVariable -= Time.deltaTime;
        }
    }
    //Fuction that handles updating slime's visuals, indicators, animations and variables based on the inherited gene.
    public void SetGene(Gene gene)
    {
        this.gene = gene;
        UpdateStatistics();
        UpdateVisuals();
        UptadeAnimations();
    }
    //Updates statistics based on the gene.
    void UpdateStatistics()
    {
        attack = gene.attack;
        maxHealth = gene.maxHealth;
        health = maxHealth;
        speed = gene.speed;
        attackSpeed = gene.attackSpeed;
        attackCooldown = gene.attackCooldown;
        bulletSpeed = gene.bulletSpeed;
        attackRange = gene.attackRange;
        visionRange = gene.visionRange;
        behaviours = gene.behaviours;
        element = gene.element;
        chargeRange = attackRange * 3;
        rangeAttackRange = attackRange * 4;

    }
    //Updates the visuals and indicators based on the gene, mostly behaviour list.
    void UpdateVisuals()
    {
        visuals[elementType].GetComponent<SpriteRenderer>().sprite = elements[Elements.elements.IndexOf(element)];
        GetComponent<SpriteRenderer>().sprite = slimes[Elements.elements.IndexOf(element)];
        GetComponent<Animator>().runtimeAnimatorController = animatorControllers[Elements.elements.IndexOf(element)];
        ScaleRange(attackRangeIndicator, attackRange);
        visuals[attackType].GetComponent<SpriteRenderer>().sprite = behavioursIcons[melee];
        visuals[movementType].GetComponent<SpriteRenderer>().sprite = behavioursIcons[walking];
        //settign up adequate bechaviour visuals
        if (behaviours.Contains("toxic"))
        {
            visuals[toxicIndicator].SetActive(true);
            toxicPs.Play(false);
        }
        if (behaviours.Contains("thorns"))
        {
            visuals[thornsIndicator].SetActive(true);
        }
        if (behaviours.Contains("charge"))
        {
            ScaleRange(chargeRangeIndicator, chargeRange);
            visuals[attackType].GetComponent<SpriteRenderer>().sprite = behavioursIcons[charge];
        }
        if (behaviours.Contains("dash"))
        {
            visuals[attackType].GetComponent<SpriteRenderer>().sprite = behavioursIcons[dash];
        }
        if (behaviours.Contains("rangeAttack"))
        {
            ScaleRange(chargeRangeIndicator, rangeAttackRange);
            visuals[attackType].GetComponent<SpriteRenderer>().sprite = behavioursIcons[ranged];
        }
        if (behaviours.Contains("flying"))
        {
            DisplayIndicator(flyingIndicator, true);
            gameObject.layer = 11;
            visuals[movementType].GetComponent<SpriteRenderer>().sprite = behavioursIcons[flying];
        }
    }
    //Function that updates particle effects and speed of animations adequatelly to "speed" variables.
    void UptadeAnimations()
    {
        {
            var temp = ps.shape;
            temp.radius *= attackRange;//scaling the explosion effect with attack range 
            ParticleSystem.MainModule elementalParticles = ps.main;
            elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(gene.element)].main.startColor.gradient);
            elementalParticles = dashPs.main;
            elementalParticles.startColor = new ParticleSystem.MinMaxGradient(ElementalParticles[Elements.elements.IndexOf(gene.element)].main.startColor.gradient);
        }
        GetComponent<Animator>().SetFloat("AttackSpeedAnim", 1 / attackSpeed);
        GetComponent<Animator>().SetFloat("AttackSpeedDashAnim", 1 / (attackSpeed * 2));
        GetComponent<Animator>().SetFloat("Charge", speed);
    }
    //Function to scale the attack, charge, range attack range with range variable.
    void ScaleRange(int indicator, float range)
    {
        visuals[indicator].SetActive(true);
        visuals[indicator].transform.localScale = new Vector2(range * 2, range * 2);
    }
    //Checks if the target, curretnly used only for player, is in range of attack. Used for ranged, melee or charge ranges. 
    bool IsInRange(Vector2 targetPosition, float range)
    {
        return Mathf.Pow(transform.position.x - targetPosition.x, 2) + Mathf.Pow(transform.position.y - targetPosition.y, 2) < Mathf.Pow(range, 2);
    }
    //Turns indicators of behaviours and visual warnings of behaviours on or off during the game.
    void DisplayIndicator(int indicator, bool b)
    {
        visuals[indicator].SetActive(b);
    }
    //Sets different state of the enemy behaviour.
    void SetState(string newState)
    {
        state = newState;
    }
    //Function responsible for taking damage. Substracting health and making slime aggressive.
    public void TakeDamage(int damage)
    {


        if (state.Equals("idle") && behaviours.Contains("charge"))
        {
            SetState("charge");
        }
        else if (state.Equals("idle"))
        {
            SetState("aggresive");
        }
        health -= damage;
        if (health <= 0)
        {
            //Passing the gene of dying slime to the collected genes list.
            GameControler.collectedGenes.Add(gene);
            Destroy(gameObject);
        }
    }
    //Reduces the counter of slimes when they die.
    private void OnDestroy()
    {
        EnemiesCounter.enemiesCounter--;
    }
    //An attack coroutine. Triggers an animation, waits for the attack speed duration and then turns on the collider responsible for dealing the damage along the particle system for the visual effect.
    IEnumerator Attack()
    {
        GetComponent<Animator>().SetTrigger("SlimeAttack");
        yield return new WaitForSeconds(attackSpeed);
        visuals[attackRangeIndicator].GetComponent<CircleCollider2D>().enabled = true;
        ps.Play();
        yield return new WaitForSeconds(ps.main.duration);

        visuals[attackRangeIndicator].GetComponent<CircleCollider2D>().enabled = false;
        SetState("idle");
        yield return null;
    }
    //A ranged attack coroutine. Triggers an animation, waits for the attack speed duration and then instantiates the bullet that deals damage to the player on collision.
    IEnumerator RangedAttack()
    {
        GetComponent<Animator>().SetTrigger("SlimeRangedAttack");
        yield return new WaitForSeconds(attackSpeed);
        firePoint.transform.right = Player.playerTransform.position - firePoint.transform.position;
        //Passing aa necessary variables to intantiated bullet.
        GameObject thisBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        EnemyBullet enemyBullet = thisBullet.GetComponent<EnemyBullet>();
        enemyBullet.bulletSpeed = bulletSpeed;
        enemyBullet.bulletDamage = attack;
        enemyBullet.bulletElement = element;
        enemyBullet.enemyParent = this;
        yield return null;
    }
    //A coroutine that makes slimes go around in random diractions when their state is idle
    IEnumerator IdleMovement()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Debug.DrawLine(transform.position, rb.velocity * 10, Color.red);
        idleMoving = true;
        for (int time = 0; time < 10; time++)
        {
            rb.velocity = new Vector2(randomX, randomY).normalized;
            yield return new WaitForSeconds(0.02f);
        }
        idleMoving = false;
        yield return null;
    }
    //A dash coroutine. Triggers an animation and then sends the slime super fast towards the player.
    //It changes the collision layer of an enemy allowing them to ignore obstacles while dashing.
    IEnumerator DashCoroutine()
    {
        DisplayIndicator(dashIndicator, true);
        GetComponent<Animator>().SetTrigger("SlimeDash");
        yield return new WaitForSeconds(attackSpeed * 2);
        isChasing = false;
        gameObject.layer = 12;
        isDashing = true;
        dashPs.Play();
        rb.AddForce((Player.playerTransform.position - transform.position).normalized * 25000f);
        DisplayIndicator(dashIndicator, false);
        yield return new WaitForSeconds(2.5f);
        isDashing = false;
        dashPs.Stop(false);
        if (gene.behaviours.Contains("flying"))
        {
            gameObject.layer = 11;
        }
        else
        {
            gameObject.layer = 7;
        }
        yield return null;
    }
    //Function responsible for the changing a slime direction based on their movement.
    void Direction()
    {
        int y;

        if (isChasing)
        {
            y = ((Player.playerTransform.position - transform.position).x > 0) ? 0 : 180;
            rb.transform.rotation = Quaternion.Euler(0, y, 0);
        }
        else if (transform.position.x != directionX)
        {
            y = ((transform.position.x - directionX) > 0) ? 0 : 180;
            rb.transform.rotation = Quaternion.Euler(0, y, 0);
        }
        directionX = transform.position.x;
    }
    //function responsible for chasing the player with a certain speed
    void ChasePlayer(float speed)
    {
        isChasing = true;
        rb.transform.position = Vector2.MoveTowards(transform.position, Player.playerTransform.position, speed * Time.deltaTime);
        Debug.DrawLine(transform.position, Player.playerTransform.position, Color.red);
    }
    //A state machine like behavioural switch case tree.
    void Behaviour()
    {
        switch (state)
        {
            case "idle":
                isChasing = false;
                DisplayIndicator(chargeIndicator, false);
                if (IsInRange(Player.playerTransform.position, visionRange))
                {
                    SetState("aggresive");
                }
                if (Random.Range(0, 500) == 0 && !idleMoving)
                {
                    StartCoroutine("IdleMovement");
                }
                break;

            case "aggresive":
                ChasePlayer(speed);
                if (!IsInRange(Player.playerTransform.position, visionRange))
                {
                    SetState("idle");
                }
                else if (IsInRange(Player.playerTransform.position, rangeAttackRange) && behaviours.Contains("rangeAttack"))
                {
                    SetState("rangeAttack");
                }
                else if (IsInRange(Player.playerTransform.position, chargeRange) && behaviours.Contains("dash") && attackCooldownVariable <= 0)
                {
                    SetState("dash");
                }
                else if (IsInRange(Player.playerTransform.position, chargeRange) && behaviours.Contains("charge"))
                {
                    SetState("charge");
                }
                else if (IsInRange(Player.playerTransform.position, attackRange))
                {
                    DisplayIndicator(chargeIndicator, false);
                    SetState("attack");
                }
                break;

            case "dash":
                if (attackCooldownVariable <= 0)
                {
                    StartCoroutine("DashCoroutine");
                    attackCooldownVariable = attackCooldown * 5f;
                }
                ChasePlayer(speed);
                break;

            case "charge":
                GetComponent<Animator>().SetFloat("Charge", speed * 1.75f);
                if (IsInRange(Player.playerTransform.position, chargeRange * 2f))
                {
                    DisplayIndicator(chargeIndicator, true);
                    ChasePlayer(speed * 1.75f);
                }
                else
                {
                    DisplayIndicator(chargeIndicator, false);
                    GetComponent<Animator>().SetFloat("Charge", speed);
                    SetState("aggresive");
                }

                if (IsInRange(Player.playerTransform.position, attackRange))
                {
                    DisplayIndicator(chargeIndicator, false);
                    GetComponent<Animator>().SetFloat("Charge", speed);
                    SetState("attack");
                }
                break;

            case "rangeAttack":
                if (IsInRange(Player.playerTransform.position, rangeAttackRange))
                {
                    if (attackCooldownVariable <= 0)
                    {
                        StartCoroutine("RangedAttack");
                        attackCooldownVariable = attackCooldown;
                    }
                }
                else
                {
                    ChasePlayer(speed);
                }
                break;

            case "attack":
                ChasePlayer(2f);
                if (attackCooldownVariable <= 0)
                {
                    StartCoroutine("Attack");
                    attackCooldownVariable = attackCooldown;
                }
                break;
        }
    }
}
