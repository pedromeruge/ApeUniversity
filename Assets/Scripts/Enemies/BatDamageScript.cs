using UnityEngine;

public class BatDamageScript : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float collisionCooldown = 1.0f;

    private float currentCollisionCooldown = 0.0f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(currentCollisionCooldown <= 0.0f){
            Vector3 objPos = transform.position;
            CallInterfaces.triggerExplosive(collider, objPos);
            CallInterfaces.hitDamageable(collider, objPos, damage);
            //CallInterfaces.signalDestructibles(collider, objPos, 0.01f);
            Debug.Log("Colliding with: "+ collider.name);
            currentCollisionCooldown = collisionCooldown;
        }
        //Destroy(gameObject); // destroy arrow on collision, regardless if wall or damageable entity
    }

    void Update()
    {
        if(currentCollisionCooldown > 0.0f){
            currentCollisionCooldown -= Time.deltaTime;
        }
    }
}