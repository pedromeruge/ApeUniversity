using UnityEngine;

public class PlayerDamage : BaseDamageable
{
    public PlayerStats playerStats;
    AudioManager audioManager;

    // Animations for player
    [SerializeField] private Animator animator;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
        bool isDead = playerStats.modifyLives(damage);
        CallDamageFlash(); // blink effect
        if (isDead) {
            animator.SetBool("isDead", true);
            audioManager.PlaySFX(audioManager.dead);
        }
        else {
            audioManager.PlaySFX(audioManager.bonk);
            animator.SetTrigger("isHurt");
        }
    }
}
