using UnityEngine;
using UnityEngine.InputSystem;
public class LadderMovement : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 8f;
    [SerializeField] private Rigidbody2D rb;
    private float initialGravityScale = 1f;
    private bool isOnLadder = false; // is next to a ladder
    private bool isClimbing = false; // is already climbing
    private float vertical;

    // Animations for player
    [SerializeField] private Animator animator;

    void Start()
    {
        initialGravityScale = rb.gravityScale; //asign the initial gravity scale to a variable, to return to it later after leaving a ladder
    }
    void Update()
    {
        if (isOnLadder && Mathf.Abs(vertical) > 0f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_emote")) { // if player on ladder and pressing up or down and not emoting
            isClimbing = true;
            animator.SetBool("isClimbing", true);
        }
    }

    //NOTE: physics related actions (that dont directly require input) should go in FixedUpdate instead of Update function
    void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
        }
        else if (!isOnLadder) {
            rb.gravityScale = initialGravityScale;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {   
            isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = false;
            isClimbing = false;
            animator.SetBool("isClimbing", false);
        }
    }

    public void Climb(InputAction.CallbackContext context) {
        
        vertical = context.ReadValue<Vector2>().y;
    }
}
