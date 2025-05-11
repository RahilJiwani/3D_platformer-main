using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public float speed; // set to 5 in the Unity Inspector
    public float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal"); //returns a decimal value between -1 and 1 based on A/D or Left/Right Arrow keys.
        float verticalMove = Input.GetAxis("Vertical"); /// same but foe W/S or Up/Down Arrow keys

        // Vector3 moveDirection is where the player is trying to move and how strongly.
        //                           // Magitude: (Horizontal Magnitude, Vertical movement, and depth)  
        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove); // Creates a direction vector using horizontal (x) and vertical (z) movement.
        // The Y-axis is set to 0, so there's no vertical movement (e.g. jumping or flying).
        
        moveDirection.Normalize(); // Normalizes the vector to ensure consistent speed regardless of direction.
        // converts moveDirection into a unit vector (length of 1), preserving the direction but ensuring consistent movement speed in all directions (so diagonals aren't faster than straight lines).
       
        float magnitude = moveDirection.magnitude; // Get the magnitude of the vector (length)
        magnitude = Mathf.Clamp(magnitude, 0 , 1); // Clamps the magnitude to a maximum of 1 to prevent faster diagonal movement.
        
        
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World); // space defines what the player is relative to 

        if (moveDirection != Vector3.zero) // Now the player can rotate to face the direction of movement, he won't snap back to default look direction
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up); // This defines the player look rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }
}
