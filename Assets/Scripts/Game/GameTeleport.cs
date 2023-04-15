using UnityEngine;
//A Teleport script.
public class GameTeleport : MonoBehaviour
{
    //Teleport that loads the next wave whenn clicked on.
    void OnMouseDown()
    {
        TimeReward();
        DrownCheck.ps.Stop(false);
        GameControler.loadNextWave();
        Destroy(gameObject);
    }
    //Function that increases the players damage by 3,2,1 or 0 based on how fast they defeated the wave.
    void TimeReward()
    {
        Player.player.GetComponent<Player>().attackDamage += (int)Mathf.Floor(Mathf.Clamp(((120 - Timer.time) / 30), 0, 3));
    }
}
