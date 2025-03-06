using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class DisablePlataformScript : MonoBehaviour
{
    [SerializeField] float disableTime = 0.2f;
    [SerializeField] private Collider2D platformCollider = null; // plataform collider
    private Collider2D playerCollider = null;
    private bool collidingWithPlatform = false;
    private void Awake() {
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            collidingWithPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {   
            collidingWithPlatform = false;
        }
    }

    private IEnumerator DisablePlataform()
    {
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(disableTime);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    public void FallOfPlataform(InputAction.CallbackContext context) {
        float vertical = context.ReadValue<Vector2>().y;
        if (context.performed && vertical < 0f && collidingWithPlatform) {
            StartCoroutine(DisablePlataform());
        }
    }
}
