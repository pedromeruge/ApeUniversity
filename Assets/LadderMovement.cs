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

    void Start()
    {
        initialGravityScale = rb.gravityScale; //asign the initial gravity scale to a variable, to return to it later after leaving a ladder
    }
    void Update()
    {
        if (isOnLadder && Mathf.Abs(vertical) > 0f) {
            isClimbing = true;
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
            print("entered Ladder trigger");
            isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            print("exited Ladder trigger");
            isOnLadder = false;
            isClimbing = false;
        }
    }

    public void Climb(InputAction.CallbackContext context) {
        vertical = context.ReadValue<Vector2>().y;
    }
}
