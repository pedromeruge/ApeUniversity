using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItemsScripts : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; // layermask that intersect with pickups
    [SerializeField] private float pickupRadius = 1.0f;
    [SerializeField] private Collider2D playerCollider = null; // player collider
    [SerializeField] private GameObject defaultPickObjectParent = null; // parent object to which picked up objects go
    [SerializeField] private GameObject defaultDropObjectParent = null; // parent object to which thrown objects go
    private IPickupable currentPickup = null; // curerntly picked up item

    GameObject findClosestPickup() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, pickupRadius, layerMask);
        GameObject closestPickup = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders) {
            IPickupable pickupObj = collider.GetComponent<IPickupable>();
            if (pickupObj != null) {
                float currDist = Vector2.Distance(this.transform.position, collider.transform.position);
                if (currDist < closestDistance) {
                    closestPickup = collider.gameObject;
                    closestDistance = currDist;
                }
            }
        }
        return closestPickup;
    }

    public void Pickup(InputAction.CallbackContext context) {
        GameObject itemToPickup = null;
        if (context.performed && currentPickup as Object == null) {
            itemToPickup = findClosestPickup();
            if (itemToPickup != null) {
                IPickupable pickupObj = itemToPickup.GetComponent<IPickupable>();
                pickupObj.OnPickup(defaultPickObjectParent);
                currentPickup = pickupObj;
            }
        }
    }

    public void Drop(InputAction.CallbackContext context) {
        if (context.performed && currentPickup != null) {
            Debug.Log("Using item" + currentPickup);
            if (currentPickup as Object == null) { // edge cases where pickup is destroyed, and still holding it
                currentPickup = null;
                return;
            }
            currentPickup.OnDrop(defaultDropObjectParent, this.transform.parent.gameObject);
            currentPickup = null;
        }
    }

    public void Use(InputAction.CallbackContext context) {
        if (context.performed && currentPickup != null) {

            if (currentPickup as Object == null) { // Handle cases where item is destroyed
                currentPickup = null;
                return;
            }

            GameObject itemObject = (currentPickup as MonoBehaviour)?.gameObject;
            if (itemObject != null) {
                DisableCollisionsTemporarily(itemObject); // disable collision before using item
            }

            bool itemDropped = currentPickup.OnUse(defaultDropObjectParent, this.transform.parent.gameObject);

            if (itemDropped) {
                currentPickup = null;
            }
        }
    }

    //disable collision between player and item for a short period of time, so that the item can be used without colliding with the player
    private void DisableCollisionsTemporarily(GameObject itemObject) {
        Collider2D itemCollider = itemObject.GetComponent<Collider2D>();

        if (itemCollider != null && playerCollider != null) {
            Physics2D.IgnoreCollision(playerCollider, itemCollider, true);
            StartCoroutine(EnableCollisionsAfterDelay(itemCollider, 0.5f)); // re-enable after delay
        }
    }

    private IEnumerator EnableCollisionsAfterDelay(Collider2D itemCollider, float delay) {
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(playerCollider, itemCollider, false); // Re-enable collision
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // Set explosion radius color to red
        Gizmos.DrawWireSphere(transform.position, pickupRadius); // Adjust radius as needed
    }
}
