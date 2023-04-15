using UnityEngine;
using UnityEngine.UI;
//A class holding a waves counter.
public class WavesCounter : MonoBehaviour
{
    public static int wavesCounter = 1;

    void Update()
    {
        UpdateCounter();
    }
    //Function that updates the wave counter display.
    void UpdateCounter()
    {
        GetComponent<Text>().text = "Wave " + wavesCounter.ToString();
    }
}
