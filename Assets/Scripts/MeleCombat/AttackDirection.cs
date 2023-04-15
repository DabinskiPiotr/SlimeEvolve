using UnityEngine;
//A class that changes the rotation of an "attack direction" of a player so it faces towards the mouse cursor. It is later used for instantiating swings and spells with correct rotation.
public class AttackDirection : MonoBehaviour
{
    public static Vector3 direction;
    Vector3 position;
    float angle;

    // Update is called once per frame
    void Update()
    {
        position = Camera.main.WorldToScreenPoint(transform.position);
        direction = Input.mousePosition - position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
