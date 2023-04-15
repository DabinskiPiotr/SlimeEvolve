using UnityEngine;
//Class responsible for the movement of camera.
public class CameraMovement : MonoBehaviour
{
    void Update()
    {
        FollowPlayer();
    }
    //A function that makes the camera follow the player.
    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.playerTransform.position.x, Player.playerTransform.position.y, -20f), 6f * Time.deltaTime);
    }
}
