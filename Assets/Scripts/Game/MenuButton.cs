using UnityEngine;
using UnityEngine.SceneManagement;
//A class that handles on click listeners for buttons.
public class MenuButton : MonoBehaviour
{
    //Initializing the wave counter to one and player starting health.
    public void StartGame()
    {
        WavesCounter.wavesCounter = 1;
        Player.maxHealth = 300;
        SceneManager.LoadScene(1);
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
