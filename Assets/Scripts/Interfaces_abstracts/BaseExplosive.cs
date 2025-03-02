using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.Collections;
public class BaseExplosive : MonoBehaviour, IExplosive
{
    [SerializeField] private float explosionRadius = 1.0f;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject explosionFxPrefab;

    [SerializeField] float cameraShakeDuration = 1.2f;
    [SerializeField] float cameraShakeStrength = 10.0f;

    public void Explode() {
        Vector3 objPos = transform.position;
        playExplosionFx(objPos);
        checkOverlapAndDestroy(objPos);
        destroySelf(objPos);

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
            destroyTerrain(collider, objPos);
            hitDamageable(collider, objPos);
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

        // Hit(damageable);

        return true;
    }

    //based on "Findable_Shelf" at https://discussions.unity.com/t/remove-tiles-given-a-point-and-a-radius/756324/6
    void destroyTerrain(Collider2D collider, Vector3 objPos) {
        Tilemap tilemap = collider.GetComponent<Tilemap>();
        Debug.Log("tilemap: " + tilemap);
        if (tilemap != null) {
            Debug.Log("got here");
            int explosionRadiusCapped = Mathf.CeilToInt(explosionRadius); // expand the search area to guarantee it covers all potential surrounding tiles
            Debug.Log("got here 2");

            for (int x = -explosionRadiusCapped; x < explosionRadiusCapped; x++)
            {
                for (int y = -explosionRadiusCapped; y < explosionRadiusCapped; y++) //find the box
                {
                    Vector3Int tilePosWorld = tilemap.WorldToCell(new Vector2(objPos.x + x, objPos.y + y));
                    Vector3 tilePosCenter = tilemap.GetCellCenterWorld(tilePosWorld); // get center of specific tile that is being exploded
                    Debug.Log("got here 3" + tilePosCenter + objPos + Vector3.Distance(objPos, tilePosCenter));

                    if (Vector2.Distance(objPos, tilePosCenter) <= explosionRadius) //check distance to make it a circle
                    {
                        Debug.Log("got here 5");
                        tilemap.SetTile(tilePosWorld, null);
                        Debug.DrawLine(new Vector3(tilePosCenter.x - 0.2f, tilePosCenter.y - 0.2f, tilePosCenter.z), new Vector3(tilePosCenter.x + 0.2f, tilePosCenter.y + 0.2f, tilePosCenter.z), Color.green, 100); // Adjust radius as needed

                    }
                }
            }
        }
    }

    //debug explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set explosion radius color to red
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Adjust radius as needed
    }
}