using UnityEngine;
using UnityEngine.InputSystem;

// based on https://www.youtube.com/watch?v=24-BkpFSZuI
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 16f;

    private bool isFacingRight = true;
    private bool isAirborn = false;

    // Animations for player
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        bool grounded = isGrounded();
        
        if (isAirborn && grounded && rb.linearVelocity.y <= 0.1f) { // if player was jumping and is now grounded, means he has landed
            isAirborn = false;
            onLanding();
        }
        else if (!grounded && rb.linearVelocity.y < -0.1f) { // if player is not grounded and is going down, means he is falling
            isAirborn = true;
            onFalling();
        }

        if (horizontal > 0f && !isFacingRight) {
            Flip();
        } else if (horizontal < 0f && isFacingRight) {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        // context performed because we only care if button is pressed, not if it is held down
        if (context.performed && isGrounded()) { 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isAirborn = true;
            animator.SetBool("isJumping", true);
            // print("Started jumping");
        }

        // if when the button is released, the player is still going up, means the player held onto the button for long, so we reduce the velocity
        // this way, holding the button longer means the player jumps higher, releasing faster means he jumps less
        if (context.canceled && rb.linearVelocity.y > 0f) { 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }
    private bool isGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

    }

    public void onLanding() {
        animator.SetBool("isJumping", false);
        // print("Landed");
    }

    private void onFalling() {
        animator.SetBool("isJumping", true);
        // print("Falling");
    }

    // keep track of horizontal input, as user moves left or right
    public void Move(InputAction.CallbackContext context) {
        horizontal = context.ReadValue<Vector2>().x;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }
}
