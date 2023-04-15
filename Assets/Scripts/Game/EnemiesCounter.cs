using UnityEngine;
using UnityEngine.UI;

//Class that is responsible for handling counter of enemies.
public class EnemiesCounter : MonoBehaviour
{
    public static int enemiesCounter;

    void Update()
    {
        UpdateCounter();
    }
    //Function that updates the enemies counter display.
    void UpdateCounter()
    {
        GetComponent<Text>().text = "x" + enemiesCounter.ToString();
    }
}

