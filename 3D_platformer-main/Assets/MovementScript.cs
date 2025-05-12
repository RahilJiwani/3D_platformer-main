using UnityEngine;
using UnityEngine.InputSystem.Android;

public class MovementScript : MonoBehaviour
{

    public float speed; // set to 5 in the Unity Inspector
    public float rotationSpeed;
    public float jumpSpeed;
    private float ySpeed;
    private CharacterController conn;
    public bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conn = GetComponent<CharacterController>();                 //the predefined component CharacterController handles collisions, slopes, stairs, etc., for you.
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

        conn.SimpleMove(moveDirection * magnitude * speed);
        
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World); // space defines what the player is relative to 

        ySpeed += Physics.gravity.y * Time.deltaTime;       //sets fall speed equal to gravity and accelerateds by 1/framerate
        if (Input.GetButtonDown("Jump")){                   //if jump button is pressed do 
            ySpeed = -0.5f;                                    //yspeed is a measure of downwards movement (fallinf by gravity), so a negative number is an upwards nudge (jump). f just sets it to a float value
            isGrounded = false;
        }

        Vector3 vel = moveDirection * magnitude;            //velocity is a vector, move direction accounts for the direction player is moving in and magnitude accounts for the magnitude portion (how fast player is moving in that direction)
        vel.y = ySpeed;                                     //Injects your vertical velocity (the result of gravity + jump) into the Y component of that velocity vector.
        //transform.Translate(vel * Time.deltaTime);          //sets translate (players' movement in the game space) to the velocity we just calculated.
        conn.Move(vel * Time.deltaTime);

        if(conn.isGrounded)
        {
            ySpeed = -0.5f;
            isGrounded = true;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                isGrounded=false;
            }
        }

        if (moveDirection != Vector3.zero) // Now the player can rotate to face the direction of movement, he won't snap back to default look direction
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up); // This defines the player look rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }
}
