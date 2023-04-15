using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public static Vector2 aim;


    Vector2 movement;

    public static int direction;
    public static Transform playerTransform;
    public static GameObject player;
    public GameObject blinkEffect;
    public GameObject dashEffect;
    float dashCooldown;
    float blinkCooldown;
    float blinkRange = 6;

    int baseSpeed = 300;
    public Element element = Element.None;

    //statistics
    public float movementSpeed;
    public static int maxHealth = 300;
    public static int health;
    public int attackDamage = 15;
    //buffs and debuffs
    public int speedModifier;
    public int slowModifier;
    float percentage = 0.01f;

    private void Awake()
    {
        player = gameObject;
        playerTransform = transform;
        //Making player not being destroyed on loading a new scene automatically.
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //Making player's current health of a value of his max health at the start of the game.
        health = maxHealth;
    }
    void Update()
    {
        if (health <= 0)
        {
            //Loading the death scene.
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
        ReadInputs();
        UpdateCounters();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerDirection();
        Dash();
        Blink();
    }
    //Function for healing a player.
    public void Heal(int heal)
    {
        health = Mathf.Clamp(health + heal, health, maxHealth);
    }
    //Function for hurting a player.
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    //Movement related inputs
    void ReadInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        playerTransform = transform;
        aim = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.transform.localPosition);

        //Cheat for testing purpouses - heals player to max health on "H" button input.
        if (Input.GetKey(KeyCode.H))
        {
            health = maxHealth;
        }
    }
    //A function responsible for updating different counters and their display.
    void UpdateCounters()
    {
        if (dashCooldown > 0)
        {
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().DashOnCooldown(true);
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().DashOnCooldown(false);
        }
        if (blinkCooldown > 0)
        {
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().BlinkOnCooldown(true);
            blinkCooldown -= Time.deltaTime;
        }
        else
        {
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().BlinkOnCooldown(false);
        }
    }
    //A function that starts the dash coroutine based on the input.
    void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && dashCooldown <= 0)
        {
            StartCoroutine("DashCor");
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().DashOnCooldown(true);
            dashCooldown = 2;
        }
    }
    //A function for teleporting a player.
    void Blink()
    {
        if (Input.GetKey(KeyCode.Space) && blinkCooldown <= 0)
        {
            Instantiate(blinkEffect, transform.position, Quaternion.identity);
            Vector2 range = Vector3.ClampMagnitude(new Vector3(aim.x, aim.y, 0), blinkRange) + transform.position;
            rb.position = Vector2.MoveTowards(rb.position, range, 100);
            Instantiate(blinkEffect, rb.position, Quaternion.identity);
            CooldownIndicators.cooldownIndicators.GetComponent<CooldownIndicators>().BlinkOnCooldown(true);
            blinkCooldown = 2;
        }
    }
    //A coroutine for the dash ability. 
    IEnumerator DashCor()
    {
        Vector2 tempAim = aim.normalized;
        for (int time = 0; time < 20; time++)
        {
            rb.AddForce(tempAim * 1000);
            GameObject temp = Instantiate(dashEffect, transform.position, Quaternion.identity);
            Destroy(temp, 0.1f);
            yield return new WaitForSeconds(0.001f);
        }
        yield return null;
    }
    //Function for moving a player.
    void PlayerMovement()
    {
        rb.MovePosition(rb.position + new Vector2(CalculateMovement(movement.x), CalculateMovement(movement.y)));
    }
    //Change of the player facing direction based on the position of a mouse cursor.
    void PlayerDirection()
    {
        int y = (aim.x > 0) ? 0 : 180;
        rb.transform.rotation = Quaternion.Euler(0, y, 0);
    }
    //Function for calculating movement speed based on different variables. Currently there are no slows and speed ups introduced in the game.
    float CalculateMovement(float direction)
    {
        return (baseSpeed + movementSpeed) * percentage * (1 + speedModifier * percentage) * (1 - Mathf.Clamp(slowModifier, 0, 100) * percentage) * direction * Time.fixedDeltaTime;
    }
    //Collision checks with different enemies. Used here for the optimisation purpouses.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" && collision.gameObject.GetComponent<Enemy>().behaviours.Contains("toxic"))
        {
            health -= collision.gameObject.GetComponent<Enemy>().attack;
            collision.gameObject.GetComponent<Enemy>().gene.damageDealt += collision.gameObject.GetComponent<Enemy>().attack;
        }
        if (collision.gameObject.tag == "enemy" && collision.gameObject.GetComponent<Enemy>().isDashing)
        {
            health -= collision.gameObject.GetComponent<Enemy>().attack;
            collision.gameObject.GetComponent<Enemy>().gene.damageDealt += collision.gameObject.GetComponent<Enemy>().attack;
            collision.gameObject.GetComponent<Enemy>().isDashing = false;
        }
    }
}
