using UnityEngine;

public class ItemDamageScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // rigidbody of the item
    [SerializeField] private int damage = 1; // how much damage the item does
    [SerializeField] private float hitForce = 5f; // speed needed to apply damage to other objects

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null) // if the object has a damageable component
        {
            // Debug.Log("Item collided with " + collision.gameObject.name + " with force " + rb.linearVelocity.magnitude);
            // if rock has a minimum impact force, damage IDamageable object
            if (rb.linearVelocity.magnitude > hitForce) {
                EventObstacle.Hit(damageable, damage);
            }
        }
    }
}
