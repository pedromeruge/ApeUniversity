using UnityEngine;

public class VasePickupableScript : BasePickupable
{
    [SerializeField] private float throwForce = 2.0f;
    [SerializeField] private float throwAngle = 30.0f;
    [SerializeField] private float breakForce = 3.0f; // velocity at which the vase breaks when colliding with ground
    private Animator animator;
    private VaseBreakLogicScript vaseBreakLogicScript;

    private new void Awake()
    {  
        base.Awake();
        vaseBreakLogicScript = GetComponent<VaseBreakLogicScript>();
        pickupHandLocalPosition = new Vector3(0.0f, 0.2f, 0.0f);
    }
    // parent to which the item goes when dropped
    public override bool OnUse(GameObject dropParent, GameObject playerParent) {

        OnDrop(dropParent);
        pickupRb.linearVelocity = new Vector2(throwForce * Mathf.Cos(throwAngle * Mathf.Deg2Rad) * playerParent.transform.localScale.x, throwForce * Mathf.Sin(throwAngle * Mathf.Deg2Rad));
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactForce = collision.relativeVelocity.magnitude; // better measure of impact force than linear velocity
        if (impactForce > breakForce)
        {
            vaseBreakLogicScript.BreakVase();
        }
    }
}
