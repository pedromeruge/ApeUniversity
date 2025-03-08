using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItemsScripts : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; // layermask that intersect with pickups
    [SerializeField] private float pickupRadius = 1.0f;
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
        Debug.Log("Picking item" + currentPickup);
        if (context.performed && currentPickup as Object == null) {
            itemToPickup = findClosestPickup();
            if (itemToPickup != null) {
                IPickupable pickupObj = itemToPickup.GetComponent<IPickupable>();
                pickupObj.OnPickup(defaultPickObjectParent);
                currentPickup = pickupObj;
                Debug.Log("Picked up: " + itemToPickup.name);
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
            currentPickup.OnDrop(defaultDropObjectParent);
            currentPickup = null;
        }
    }

    public void Use(InputAction.CallbackContext context) {
        if (context.performed && currentPickup != null) {
            Debug.Log("Using item" + currentPickup);
            if (currentPickup as Object == null) { // edge cases where pickup is destroyed, and still holding it
                currentPickup = null;
                return;
            }
            bool itemDropped = currentPickup.OnUse(defaultDropObjectParent, this.transform.parent.gameObject); // hardcoded player parent, could be better ...
            if (itemDropped) {
                currentPickup = null;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // Set explosion radius color to red
        Gizmos.DrawWireSphere(transform.position, pickupRadius); // Adjust radius as needed
    }
}
