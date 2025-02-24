using UnityEngine;

public class Player : BaseDamageable
{
    public PlayerStats playerStats;

    // Animations for player
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        BaseObstacle.OnObstacleHit += OnObstacleHit;
    }

    private void OnDisable()
    {
        BaseObstacle.OnObstacleHit -= OnObstacleHit;
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
        bool isDead = playerStats.modifyLives(damage);
        CallDamageFlash(); // blink effect
        if (isDead) {
            animator.SetBool("isDead", true);
        }
        else {
            animator.SetTrigger("isHurt");
        }
    }
}
