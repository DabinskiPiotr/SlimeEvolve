using UnityEngine;
using UnityEngine.UI;
//A Script responsible for updating the timer during the game.
public class Timer : MonoBehaviour
{
    public static float time;
    public Text timer;
    public static bool countTime;

    void Start()
    {
        countTime = true;
        time = 0;
    }
    void FixedUpdate()
    {
        DisplayTimer();
    }
    //Function for counting and calculating the time into the Hours, Minutes and Seconds format.
    void DisplayTimer()
    {
        if (countTime)
        {
            time += Time.deltaTime;
        }
        int seconds = (int)(time % 60);
        int minutes = (int)(time / 60) % 60;
        int hours = (int)(time / 3600);
        timer.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
