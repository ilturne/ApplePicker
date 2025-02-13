using UnityEngine;

public class BasketController : MonoBehaviour
{
    private float moveSpeed = 250f;
    private float screenLeftX;
    private float screenRightX;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calculate screen boundaries in world space
        if (Camera.main != null)
        {
            screenLeftX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            screenRightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        }
        else
        {
            Debug.LogError("Main Camera not found.");
        }
    }

    // Use Update() to read input from the legacy Input Manager
    private void Update()
    {
        // Get horizontal input from the keyboard (or joystick)
        float horizontalInput = Input.GetAxis("Horizontal");
        moveInput = new Vector2(horizontalInput, 0);

        // Update the IsMoving property
        IsMoving = moveInput != Vector2.zero;
    }

    // Use FixedUpdate() for physics-based movement
    private void FixedUpdate()
    {
        // Apply horizontal movement based on player input
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);

        // Clamp the basket's position to stay within screen boundaries (with 1 unit padding)
        float clampedX = Mathf.Clamp(transform.position.x, screenLeftX + 1.0f, screenRightX - 1.0f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Stop movement if the basket is at the edge and trying to move further
        if ((transform.position.x <= screenLeftX + 1.0f && moveInput.x < 0) ||
            (transform.position.x >= screenRightX - 1.0f && moveInput.x > 0))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    // Handles collisions with falling objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Rotten Apple"))
        {
            GameManager.Instance.RemoveScore(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Branch"))
        {
            GameManager.Instance.GameOver();
            Destroy(other.gameObject);
        }
    }
}