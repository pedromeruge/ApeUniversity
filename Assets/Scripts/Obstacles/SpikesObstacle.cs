using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class SpikesObstacle: MonoBehaviour
{
    [SerializeField] 
    private int damage = 1;

    [SerializeField]
    private Tilemap spikeTilemap; // reference to the tilemap that contains the spikes

    
    private void OnTriggerEnter2D(Collider2D collider)
    {   
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null) // if the object has a damageable component
        {   
            //couldnt manage to get the contact direction from the cells position working, used the rigidbody instead
            // Vector3 collisionPoint = collider.ClosestPoint(transform.position); //more accurate than collider.transform.position
            // Vector3Int cellCoordinate = spikeTilemap.WorldToCell(collisionPoint); //convert collider point to cell coordinate in the tilemap
            // Vector3 cellCenter = spikeTilemap.GetCellCenterWorld(cellCoordinate); //get the center of the specific cell that was hit

            Vector2 vel = damageable.GetRigidbody().linearVelocity;
            if (vel.y < 0 && Mathf.Abs(vel.y) > Mathf.Abs(vel.x)) {
                //Only trigger hit collider if the object has more downward velocity than horizontal
                EventObstacle.Hit(damageable, damage);
            }
        }
    }
}
