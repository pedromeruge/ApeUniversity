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

    //Coyote time
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0f;

    //Jump buffering
    [SerializeField] private float jumpBufferTime = 0.1f;
    private float jumpBufferTimeCounter = 0f;

    // Animations for player
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        bool grounded = isGrounded();
        
        //calculate player airborn status
        if (isAirborn && grounded && rb.linearVelocity.y <= 0.1f) { // if player was jumping and is now grounded, means he has landed
            isAirborn = false;
            onLanding();
        }
        else if (!grounded && rb.linearVelocity.y < -0.1f) { // if player is not grounded and is going down, means he is falling
            isAirborn = true;
            onFalling();
        }

        //flip player based on direction
        if (horizontal > 0f && !isFacingRight) {
            Flip();
        } else if (horizontal < 0f && isFacingRight) {
            Flip();
        }

        //coyote time calculation
        if (grounded) {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }


        //jump buffer time calculation
        jumpBufferTimeCounter -= Time.deltaTime;

        if (jumpBufferTimeCounter > 0f && coyoteTimeCounter > 0f) { // if player has pressed jump button in normal/buffer time or in coyote time, he can jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isAirborn = true;
            jumpBufferTimeCounter = 0f; // reset jump buffer time
            animator.SetBool("isJumping", true);
            // print("Started jumping");
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        // context performed because we only care when button starts being pressed, not if it is held down
        if (context.performed) {
            jumpBufferTimeCounter = jumpBufferTime; // if player presses jump button, start the jump buffer time
        }

        // if when the button is released, the player is still going up, means the player held onto the button for long, so we reduce the velocity
        // this way, holding the button longer means the player jumps higher, releasing faster means he jumps less
        if (context.canceled && rb.linearVelocity.y > 0f) { 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

            coyoteTimeCounter = 0f; // if player releases the button, coyote time is over, no double jumping allowed
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
