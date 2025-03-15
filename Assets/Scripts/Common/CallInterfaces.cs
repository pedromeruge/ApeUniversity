using UnityEngine;

public class CallInterfaces : MonoBehaviour
{
    public static bool hitDamageable(Collider2D collider, Vector3 objPos, int damage) {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable == null) {
            return false;
        }

        EventObstacle.Hit(damageable, damage);

        return true;
    }

    //based on "Findable_Shelf" at https://discussions.unity.com/t/remove-tiles-given-a-point-and-a-radius/756324/6
    public static bool signalDestructibles(Collider2D collider, Vector3 objPos, float explosionRadius) {
        IDestructible destructible = collider.GetComponent<IDestructible>();
        if (destructible == null) {
            return false;
        }
        EventDestroy.Destroy(destructible, objPos, explosionRadius);
        return true;
    }

    //trigger any other explosive objects nearby, without giving explosion cooldown
    public static bool triggerExplosive(Collider2D collider, Vector3 objPos, GameObject explodingGameObject=null) {
        IExplosive explosive = collider.GetComponent<IExplosive>();
        if (explosive == null || explodingGameObject == null || (Object)explosive == explodingGameObject)
        {
            return false;
        }
        EventExplode.Explode(explosive);
        return true;
    }

    public static void knockbackObjects(Collider2D collider, Vector3 objPos, float explosionKnockbackForce) {
        IKnockbackable knockbackableObj = collider.GetComponent<IKnockbackable>();
        if (knockbackableObj == null) {
            return;
        }
        // Debug.Log("Knocking back: " + collider.name);
        Vector3 knockbackForce = (knockbackableObj.getTransform().position - objPos) * explosionKnockbackForce;
        knockbackableObj.Knockback(knockbackForce);
    }
}
