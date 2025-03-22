using UnityEngine;
using UnityEngine.Tilemaps;

// flag a tilemap as destructible by explosives
public abstract class BasePickupable : MonoBehaviour, IPickupable
{
    protected Collider2D pickupCollider;
    protected Rigidbody2D pickupRb;

    protected Vector3 pickupHandLocalPosition = Vector3.zero; // vector to add onto the player's hand position when picking up the object, so it looks like it's being held
    protected void Awake()
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
        this.transform.localPosition = pickupHandLocalPosition;
        this.transform.localRotation = Quaternion.identity;

    }
    
    public void OnDrop(GameObject dropParent, GameObject playerParent) {
        pickupCollider.enabled = true;
        this.transform.position = playerParent.transform.position; // make dropped object occupy center of playe position, so when dropping items next to walls, they dont fall into the map
        pickupRb.bodyType = RigidbodyType2D.Dynamic;
        this.transform.SetParent(dropParent.transform, true);
    }

    public void setParent(Transform parent)
    {
        this.transform.parent = parent;
    }
}

