using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//A Game Controler class that is responsible for a lot of general game functions
public class GameControler : MonoBehaviour
{
    public static List<Gene> collectedGenes;
    public GameObject portal;
    bool once;
    float infoTime;
    public static Elements elementCalculator;
    public Text text;
    public static string[] colors = { "#192226", "#D01716", "#283593", "#6BC300", "#5D4037", "#FBC02D", "#8DFDFF", "#FFF176", "#8B93AF" };
    private void Awake()
    {
        //Setting collisions checks between approperiate layers.
        //Physics2D.IgnoreLayerCollision(7, 7, true); additional setting so enemies can't collide with each other
        Physics2D.IgnoreLayerCollision(6, 3, true);
        Physics2D.IgnoreLayerCollision(10, 3, false);
        Physics2D.IgnoreLayerCollision(11, 4, true);
        Physics2D.IgnoreLayerCollision(11, 8, true);
        Physics2D.IgnoreLayerCollision(11, 7, true);
        Physics2D.IgnoreLayerCollision(12, 8, true);
        Physics2D.IgnoreLayerCollision(12, 7, true);
        Physics2D.IgnoreLayerCollision(12, 11, true);
        Physics2D.IgnoreLayerCollision(13, 8, true);
        Physics2D.IgnoreLayerCollision(13, 6, true);
        Physics2D.IgnoreLayerCollision(13, 4, true);
        Physics2D.IgnoreLayerCollision(13, 3, true);
        DrowningCollisions();
        elementCalculator = new Elements();
    }
    //Setting the target frame rate to 300. Initializing the collected gene list.
    void Start()
    {
        Application.targetFrameRate = 300;
        collectedGenes = new List<Gene>();
        once = true;
    }
    void Update()
    {
        WaveFinished();
    }
    //Setting up the elemental calculator multiplier
    public static float getElementalMultiplier(Element elementDeal, Element elementRecieve)
    {
        return elementCalculator.DamageMultiplier(elementDeal, elementRecieve);
    }
    //Function for loading the next wave. Responsible for sorting the list of collected genes base on the comparer. Currently Descending.
    public static void loadNextWave()
    {
        DamageComparer damageComparer = new DamageComparer();
        collectedGenes.Sort(damageComparer);
        SceneManager.LoadScene(1);
        WavesCounter.wavesCounter++;
    }
    //A function that checks if the wave has finished. It turns off counting of time and displays the message after end of a wave additionally spawning the portal to the next one.
    void WaveFinished()
    {
        int enemies = EnemiesCounter.enemiesCounter;
        if (enemies == 0 && Timer.time > 1 && once)
        {
            Timer.countTime = false;
            Instantiate(portal, ArenaGenerator.portalSpawnPoint, Quaternion.identity);
            text.GetComponent<Text>().enabled = true;
            infoTime = 5f;
            once = false;

        }
        if (text.GetComponent<Text>().enabled == true && infoTime < 0)
        {
            text.GetComponent<Text>().enabled = false;
        }
        infoTime -= Time.deltaTime;
    }
    //Function that turns the drowning layer collisions only for the water with "drownCheck" colliders.
    void DrowningCollisions()
    {
        for (int i = 0; i < 31; i++)
        {
            Physics2D.IgnoreLayerCollision(i, 31, true);
        }
        Physics2D.IgnoreLayerCollision(4, 31, false);
    }
}
