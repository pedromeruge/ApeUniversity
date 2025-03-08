using UnityEngine;

public class VaseHitScript : BaseHittable
{
    private VaseBreakLogicScript vaseBreakLogicScript;

    private void Awake()
    {
        vaseBreakLogicScript = GetComponent<VaseBreakLogicScript>();
    }

    private void OnEnable()
    {
        PlayerHitScript.OnEntityHit += OnVaseHit;
    }

    private void OnDisable()
    {
        PlayerHitScript.OnEntityHit -= OnVaseHit;
    }

    private void OnVaseHit(IHittable target, PlayerStats playerStats)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this) {
            OnHit();
        }
    }

    public override void OnHit()
    {
        vaseBreakLogicScript.BreakVase();
    }
}
