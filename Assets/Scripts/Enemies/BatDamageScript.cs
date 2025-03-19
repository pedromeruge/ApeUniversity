using System;
using UnityEngine;

public class BatDamageScript : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float collisionCooldown = 1.0f;

    private float lastCollisionTime= -Mathf.Infinity;

    void OnTriggerStay2D(Collider2D collider) // runs every frame while object inside trigger, to reapply damage even if still in cooldown
    {
        if (!collider.CompareTag("Player")) return; // guarantee it only hits players

        if(Time.time >= lastCollisionTime + collisionCooldown){
            Vector3 objPos = transform.position;
            CallInterfaces.triggerExplosive(collider, objPos);
            CallInterfaces.hitDamageable(collider, objPos, damage);
            //CallInterfaces.signalDestructibles(collider, objPos, 0.01f);
            Debug.Log("Colliding with: "+ collider.name);
            lastCollisionTime = Time.time;
        }
        //Destroy(gameObject); // destroy arrow on collision, regardless if wall or damageable entity
    }
}