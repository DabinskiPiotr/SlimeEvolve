using UnityEngine;
//Visual effect of the blink spell.
public class BlinkEffect : MonoBehaviour
{
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
}
