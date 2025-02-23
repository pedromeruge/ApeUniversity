using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerStats playerStats;

    // Animations for player
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        ObstacleEvent.OnObstacleHit += OnObstacleHit;
    }

    private void OnDisable()
    {
        ObstacleEvent.OnObstacleHit -= OnObstacleHit;
    }

    private void OnObstacleHit(IDamageable target, int damage)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this)
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage = -1)
    {
        bool isDead = playerStats.modifyLives(damage);
        if (isDead) {
            animator.SetBool("isDead", true);
        }
        else {
            Debug.Log("Triggering Hurt Animation");
            animator.SetTrigger("isHurt");
            animator.SetBool("isHurt_0", true);
        }
    }
}
