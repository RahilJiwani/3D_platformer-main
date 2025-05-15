using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform transformObj; // the object to follow (the player)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, transformObj.position,10 * Time.deltaTime); // returns a position closer to the target (player)
        // this.transform.position is the enemy's position
        // transformObj.position this is the player's position
        // 10 * Time.deltaTime is the speed of the enemy
    }
}
