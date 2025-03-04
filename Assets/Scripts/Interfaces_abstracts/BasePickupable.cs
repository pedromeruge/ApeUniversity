using UnityEngine;
using UnityEngine.Tilemaps;

// flag a tilemap as destructible by explosives
public abstract class BasePickupable : MonoBehaviour, IPickupable
{
    protected Collider2D pickupCollider;
    protected Rigidbody2D pickupRb;
    void Awake()
    {
        pickupCollider = this.GetComponent<Collider2D>();
        pickupRb = this.GetComponent<Rigidbody2D>();
    }
    
    
    public abstract bool OnUse(GameObject dropParent, GameObject playerParent);
    
    public void OnPickup(GameObject playerHandParent) {
        pickupCollider.enabled = false;
        //reset movement parameters (so they dont stack on consecutive pickups)
        pickupRb.linearVelocity = Vector2.zero;
        pickupRb.angularVelocity = 0.0f;
        pickupRb.rotation = 0.0f;
        pickupRb.bodyType = RigidbodyType2D.Kinematic;

        this.transform.SetParent(playerHandParent.transform, true);

        // Reposition the object to the center of the player's hand
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

    }
    
    public void OnDrop(GameObject dropParent) {
        pickupCollider.enabled = true;
        pickupRb.bodyType = RigidbodyType2D.Dynamic;
        this.transform.SetParent(dropParent.transform, true);
    }
    

    public void setParent(Transform parent)
    {
        this.transform.parent = parent;
    }
}

