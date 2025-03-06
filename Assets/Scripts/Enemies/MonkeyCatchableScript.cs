using UnityEngine;

public class MonkeyCatchableScript : BaseHittable
{
    private void OnEnable()
    {
        PlayerHitScript.OnEntityHit += OnEntityHit;
    }

    private void OnDisable()
    {
        PlayerHitScript.OnEntityHit -= OnEntityHit;
    }

    private void OnEntityHit(IHittable target)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this) {
            OnHit();
        }
    }

    public override void OnHit()
    {
        Destroy(this.gameObject);
    }
}

