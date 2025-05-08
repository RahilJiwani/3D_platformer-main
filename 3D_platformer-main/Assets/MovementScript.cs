using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Tooltip("Units per second")]
    public float speed = 5f;
    //penis

    void Update()
    {
        // read raw axis values (–1 to +1 from arrows or WASD)
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        // debug: log whenever we actually get meaningful input
        if (Mathf.Abs(horizontalMove) > 0.01f || Mathf.Abs(verticalMove) > 0.01f)
        {
            Debug.Log($"Got input H={horizontalMove:0.00}, V={verticalMove:0.00}");
        }

        // build movement vector
        Vector3 moveDirection = new Vector3(horizontalMove, 0f, verticalMove);

        // apply movement
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
}
