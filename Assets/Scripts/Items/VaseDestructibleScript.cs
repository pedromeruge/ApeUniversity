using UnityEngine;

public class VaseDestructibleScript : MonoBehaviour, IDestructible
{
    private VasePickupableScript vasePickupableScript;

    private void Awake()
    {
        vasePickupableScript = GetComponent<VasePickupableScript>();
    }

    private void OnEnable()
    {
        EventDestroy.OnDestroy += HandleDestroy;
    }

    private void OnDisable()
    {
        EventDestroy.OnDestroy -= HandleDestroy;
    }

    private void HandleDestroy(IDestructible target, Vector3 objPos, float radius)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this)
        {
            Destroy(objPos, radius);
        }
    }
    
    //need a function with these params to match interface
    public void Destroy(Vector3 destroyPos, float destroyRadius)
    {
        vasePickupableScript.BreakVase();
    }
}
