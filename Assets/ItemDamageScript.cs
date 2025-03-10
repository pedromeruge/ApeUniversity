using UnityEngine;

public class ItemDamageScript2 : MonoBehaviour
{
    [SerializeField] private int damage = 1; // how much damage the item does
    [SerializeField] private float breakForce = 5f; // speed needed to apply damage to other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null) // if the object has a damageable component
        {
            Debug.Log("Item collided with " + collision.gameObject.name);
            float impactForce = collision.relativeVelocity.magnitude;
            // if rock has a minimum impact force, damage IDamageable object
            if (impactForce > breakForce) {
                EventObstacle.Hit(damageable, damage);
            }
        }
    }
}
