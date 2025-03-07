using UnityEngine;

public class MonkeyCatchableScript : BaseHittable
{
    [SerializeField] private Animator anim;
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
        if (anim == null)
        {
            Debug.LogError("Animator is not assigned!", this);
            return;
        }
        anim.SetBool("isRunning",false);
        anim.SetBool("isDead",true);
        Destroy(this.gameObject,5f);
    }
}
