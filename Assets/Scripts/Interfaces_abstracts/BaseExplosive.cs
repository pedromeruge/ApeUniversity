using UnityEngine;
using UnityEngine.Tilemaps;
public class BaseExplosive : MonoBehaviour, IExplosive
{
    [SerializeField] private float explosionRadius = 1.0f;

    [SerializeField] private float explosionKnockbackForce = 10.0f;
    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject explosionFxPrefab;

    [SerializeField] float cameraShakeDuration = 1.2f;
    [SerializeField] float cameraShakeStrength = 10.0f;
    [SerializeField] protected int damage = 1;
    private bool hasExploded = false;


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
        if (hasExploded) {
            return;
        }
        hasExploded = true; // prevent multiple explosions
        Debug.Log("Exploding with damage: " + damage);
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
            CallInterfaces.triggerExplosive(collider, objPos, this.gameObject);
            CallInterfaces.hitDamageable(collider, objPos, damage);
            CallInterfaces.signalDestructibles(collider, objPos, explosionRadius);
            CallInterfaces.knockbackObjects(collider, objPos, explosionKnockbackForce);
        }
    }

    void destroySelf(Vector3 objPos) {
        // destroy the prefab
        Destroy(this.gameObject); 
    }

    //debug explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set explosion radius color to red
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Adjust radius as needed
    }
}