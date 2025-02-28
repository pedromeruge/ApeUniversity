using UnityEngine;

public class CatchableScript : BaseCatchable
{
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
        Destroy(this.gameObject);
    }
}

