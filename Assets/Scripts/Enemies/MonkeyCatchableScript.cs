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

    private void OnEntityHit(IHittable target, PlayerStats playerStats)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this) {
            playerStats.modifyMonkeys(1); // add monkey to player stats
            OnHit();
        }
    }

    public override void OnHit()
    {
        Destroy(this.gameObject);
    }
}

