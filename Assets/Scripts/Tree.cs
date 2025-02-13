using UnityEngine;

public class Tree : MonoBehaviour
{
    public float moveSpeed = 50f;
    private Vector2 movementDirection = Vector2.right;
    private Rigidbody2D rb;

    private float screenLeftX; // World-space left boundary
    private float screenRightX; // World-space right boundary
    private float buffer = 1.0f; // Buffer zone to prevent jittering

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        screenLeftX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Mathf.Abs(Camera.main.transform.position.z))).x;
        screenRightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Mathf.Abs(Camera.main.transform.position.z))).x;
    }

    private void FixedUpdate()
    {
        // Move the tree using its current direction
        rb.linearVelocity = new Vector2(movementDirection.x * moveSpeed, rb.linearVelocityY);

        if (transform.position.x < screenLeftX + buffer)
        {
            movementDirection = Vector2.right; 
        }
        else if (transform.position.x > screenRightX - buffer)
        {
            movementDirection = Vector2.left;
        }
    }
}
