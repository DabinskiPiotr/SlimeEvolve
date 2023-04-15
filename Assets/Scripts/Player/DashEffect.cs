using UnityEngine;
//A visual effect of the dash ability.
public class DashEffect : MonoBehaviour
{
    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r + 100f, GetComponent<SpriteRenderer>().color.g + 100f, GetComponent<SpriteRenderer>().color.b + 100f);
    }
}
