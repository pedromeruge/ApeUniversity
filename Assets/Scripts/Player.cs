using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerStats playerStats;

    // Animations for player
    [SerializeField] private Animator animator;

    //Rigidbody 2d
    [SerializeField] private Rigidbody2D rb;

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

    public void TakeDamage(int damage = -1)
    {
        bool isDead = playerStats.modifyLives(damage);
        if (isDead) {
            animator.SetBool("isDead", true);
        }
        else {
            animator.SetTrigger("isHurt");
        }
    }

    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }
}
