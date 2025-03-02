using UnityEngine;
using UnityEngine.Tilemaps;
public class BaseExplosive : MonoBehaviour, IExplosive
{
    [SerializeField] private float explosionRadius = 1.0f;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject explosionFxPrefab;

    [SerializeField] float cameraShakeDuration = 1.2f;
    [SerializeField] float cameraShakeStrength = 10.0f;

    //also immediately destroy the bomb, when another explosion occurs near
    private void OnEnable()
    {
        EventExplode.OnExplode += HandleExplode;
    }

    private void OnDisable()
    {
        EventExplode.OnExplode -= HandleExplode;
    }

    private void HandleExplode(IExplosive target)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this)
        {
            Explode();
        }
    }

    public void Explode() {
        Vector3 objPos = transform.position;
        playExplosionFx(objPos);
        destroySelf(objPos);
        checkOverlapAndDestroy(objPos);

        Debug.DrawLine(new Vector3(objPos.x - 0.2f, objPos.y + 0.2f, objPos.z), new Vector3(objPos.x + 0.2f, objPos.y - 0.2f, objPos.z), Color.red, 100); // Adjust radius as needed
    }
    
    // play the explosion FX that appears in front of the bomb
    void playExplosionFx(Vector3 objPos) {
        GameObject explosionFxInstance = Instantiate(explosionFxPrefab, objPos + explosionFxPrefab.transform.position, Quaternion.identity); // load explosion prefab // add prefab position to set correct z value
        CameraShake.Shake(cameraShakeDuration, cameraShakeStrength); // shake camera
        Animator animator = explosionFxInstance.GetComponent<Animator>();
        float explosionDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosionFxInstance, explosionDuration); // destroy object after animation finishes
    }

    void checkOverlapAndDestroy(Vector3 objPos) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(objPos, explosionRadius, layerMask);
        foreach (Collider2D collider in colliders) {
            Debug.Log("Explosion hit: " + collider.name);
            triggerExplosive(collider, objPos);
            hitDamageable(collider, objPos);
            destroyTerrain(collider, objPos);
        }
    }

    void destroySelf(Vector3 objPos) {
        // destroy the prefab
        Destroy(this.gameObject); 
    }

    bool hitDamageable(Collider2D collider, Vector3 objPos) {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable == null) {
            return false;
        }

        EventObstacle.Hit(damageable, 1);

        return true;
    }

    //based on "Findable_Shelf" at https://discussions.unity.com/t/remove-tiles-given-a-point-and-a-radius/756324/6
    bool destroyTerrain(Collider2D collider, Vector3 objPos) {
        IDestructible destructible = collider.GetComponent<IDestructible>();
        if (destructible == null) {
            return false;
        }
        EventDestroy.Destroy(destructible, objPos, explosionRadius);
        return true;
    }

    //trigger any other explosive objects nearby, without giving explosion cooldown
    bool triggerExplosive(Collider2D collider, Vector3 objPos) {
        IExplosive explosive = collider.GetComponent<IExplosive>();
        if (explosive == null || (Object)explosive == this)
        {
            return false;
        }
        EventExplode.Explode(explosive);
        return true;
    }

    //debug explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set explosion radius color to red
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Adjust radius as needed
    }
}