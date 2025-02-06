using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Basket_Controller : MonoBehaviour
{
    public float moveSpeed = 250f; 
    private float screenLeftX;    
    private float screenRightX;  
    private Vector2 moveInput;    
    public bool IsMoving { get; private set; } 
    private Rigidbody2D rb;     

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        screenLeftX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            GameManager.Instance.addScore(1);

            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Rotten Apple"))
        {
            GameManager.Instance.removeScore(1);

            Destroy(other.gameObject);
        }
    }
    private void FixedUpdate()
    {
        // Move the basket based on player input
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);

        // Clamp the basket's position to stay within screen boundaries
        float clampedX = Mathf.Clamp(transform.position.x, screenLeftX + 1.0f, screenRightX - 1.0f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Stop movement if the basket is at the edge and trying to move further
        if (transform.position.x == screenLeftX + 1.0f && moveInput.x < 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else if (transform.position.x == screenRightX - 1.0f && moveInput.x > 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}
