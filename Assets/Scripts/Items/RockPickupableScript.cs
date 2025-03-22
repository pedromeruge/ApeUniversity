using UnityEngine;

public class RockPickupableScript : BasePickupable
{
    [SerializeField] private float throwForce = 10.0f;
    [SerializeField] private float throwRotation = 10.0f;
    [SerializeField] private float throwAngle = 45.0f;

    // parent to which the item goes when dropped
    public override bool OnUse(GameObject dropParent, GameObject playerParent) {

        OnDrop(dropParent, playerParent);
        pickupRb.linearVelocity = new Vector2(throwForce * Mathf.Cos(throwAngle * Mathf.Deg2Rad) * playerParent.transform.localScale.x, throwForce * Mathf.Sin(throwAngle * Mathf.Deg2Rad));
        pickupRb.angularVelocity = throwRotation;
        return true;
    }
}
