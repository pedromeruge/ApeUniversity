using UnityEngine;

public class CatchableScript : BaseCatchable
{

    [SerializeField] private Animator anim;
    private void OnEnable()
    {
        PlayerCatchScript.OnEntityCaught += OnEntityCaught;
    }

    private void OnDisable()
    {
        PlayerCatchScript.OnEntityCaught -= OnEntityCaught;
    }

    private void OnEntityCaught(ICatchable target)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this) {
            OnCaught();
        }
    }

    public override void OnCaught()
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

