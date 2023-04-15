using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//A Script attached to the deathSlime in the death scene. Handles the display of everyting changable in the scene. The count of waves and loading the menu.
public class DeathSlime : MonoBehaviour
{
    public GameObject deadPlayer;
    public Text text;
    private void Start()
    {
        text.text = "You Died on wave number " + WavesCounter.wavesCounter.ToString();
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0.015f, 0, 0));
        if (transform.position.x > 0.25)
        {
            deadPlayer.transform.Translate(new Vector3(0.015f, 0, 0));
        }
        if (transform.position.x > 5)
        {
            SceneManager.LoadScene(0);
        }
    }
}
