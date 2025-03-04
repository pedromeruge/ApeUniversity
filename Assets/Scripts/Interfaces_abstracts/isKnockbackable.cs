using UnityEngine;
using UnityEngine.Tilemaps;

// flag a tilemap as destructible by explosives
public class isKnockbackable : MonoBehaviour, IKnockbackable
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Vector3 force)
    {
        // Debug.Log("Got Knockback force: " + force);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public Transform getTransform()
    {
        return this.transform;
    }
}

