using UnityEngine;

public class MonkeyDamageableScript : BaseDamageable
{
    [SerializeField] private MonkeyCatchableScript monkeyCatchableScript;

    private void OnEnable()
    {
        EventObstacle.OnObstacleHit += OnObstacleHit;
    }

    private void OnDisable()
    {
        EventObstacle.OnObstacleHit -= OnObstacleHit;
    }

    private void OnObstacleHit(IDamageable target, int damage)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this)
        {
            TakeDamage(damage);
        }
    }

    public override void TakeDamage(int damage = -1)
    {
        CallDamageFlash(); // blink effect
        monkeyCatchableScript.OnHit();
    }
}
