using UnityEngine;
using UnityEngine.Tilemaps;

// flag a tilemap as destructible by explosives
public class isPickupable : MonoBehaviour, IPickupable
{
    public void OnPickup(GameObject playerHandParent) {
        this.GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        //reset movement parameters (so they dont stack on consecutive pickups)
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
        rb.rotation = 0.0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        this.transform.SetParent(playerHandParent.transform, true);

        // Reposition the object to the center of the player's hand
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

    }
    
    public void OnDrop(GameObject sceneParent) {
        this.GetComponent<Collider2D>().enabled = true;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.transform.SetParent(sceneParent.transform, true);
    }
    
    public void setParent(Transform parent)
    {
        this.transform.parent = parent;
    }

    private void OnEnable()
    {
        EventDestroy.OnDestroy += HandleDestroy;
    }

    private void OnDisable()
    {
        EventDestroy.OnDestroy -= HandleDestroy;
    }

    private void HandleDestroy(IDestructible target, Vector3 objPos, float radius)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this)
        {
            Destroy(objPos, radius);
        }
    }
    public void Destroy(Vector3 destroyPos, float destroyRadius)
    {
    }
}

