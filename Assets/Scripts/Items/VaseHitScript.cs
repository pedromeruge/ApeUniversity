using UnityEngine;

public class VaseHitScript : BaseHittable
{
    private VasePickupableScript vasePickupableScript;

    private void Awake()
    {
        vasePickupableScript = GetComponent<VasePickupableScript>();
    }

    private void OnEnable()
    {
        PlayerHitScript.OnEntityHit += OnVaseHit;
    }

    private void OnDisable()
    {
        PlayerHitScript.OnEntityHit -= OnVaseHit;
    }

    private void OnVaseHit(IHittable target)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this) {
            OnHit();
        }
    }

    public override void OnHit()
    {
        vasePickupableScript.BreakVase();
    }
}
