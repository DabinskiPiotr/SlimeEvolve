using UnityEngine;
//A script responsible for checking and displaing the visual effect of drowning.
public class DrownCheck : MonoBehaviour
{
    public static ParticleSystem ps;
    GameObject player;
    float swimTime = 5f;
    float damageInterval = 0.5f;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        player = transform.root.gameObject;
    }
    //Counting time spent in the water in a row.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            if (ps.isPlaying == false)
            {
                ps.Play();
            }
            swimTime -= Time.deltaTime;
            damageInterval -= Time.deltaTime;
            if (swimTime < 0 && damageInterval < 0)
            {
                Drown();
            }
        }
    }
    //Reseting counters if the player leaves the water.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            ps.Stop(false);
            swimTime = 5f;
            damageInterval = 0.5f;
        }
    }
    //Dealing damage to the player every 0.5 second if they stay in the water for too long.
    void Drown()
    {
        player.GetComponent<Player>().TakeDamage(20);
        damageInterval = 0.5f;
    }
}
