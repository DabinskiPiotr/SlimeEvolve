using UnityEngine;
//A Script that handles visuals of destroying the destructables.
public class Destructables : MonoBehaviour
{
    public void DestroyHandler()
    {
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
}
