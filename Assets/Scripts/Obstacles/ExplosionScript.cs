using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.Collections;
public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private int countdown = 2;
    [SerializeField] private float explosionRadius = 1.0f;

    [SerializeField] LayerMask layerMask;

    [SerializeField] Tilemap tilemap; // tilemap of mines, to pinpoint which mine exploded

    void Awake()
    {
        if (tilemap == null) {
            tilemap = GetComponent<Tilemap>();
        }
        if (tilemap == null)
        {
            Debug.LogWarning("Tilemap not found for mines");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Vector3 hitPosition = collider.bounds.center; // center position of the object that collided with the bomb, at the time of collision
        // Get the bottom-center of the mine's collider
        Vector3 hitPosition = new Vector3(collider.bounds.center.x, collider.bounds.min.y, collider.bounds.center.z);
        Debug.Log("collider Center: " + hitPosition + " collider bounds: " + collider.bounds);
        Vector3Int tilePosition = tilemap.WorldToCell(hitPosition);
        Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePosition); // get center of specific tile that was triggered
        Debug.Log("tileCenter: " + tileCenter);
        Debug.Log(collider.name + " entered explosion");
        StartCoroutine(countdownExplosion(countdown, tilePosition));
    }

    IEnumerator countdownExplosion(int seconds, Vector3Int tilePosition) {
        yield return new WaitForSeconds(seconds);
        Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePosition); // get center of specific tile that was triggered
        Debug.Log("Explosion at: " + tileCenter);
        checkOverlap(tileCenter);
        tilemap.SetTile(tilePosition, null); // remove specific mine triggered
        Debug.DrawLine(new Vector3(tileCenter.x - 0.2f, tileCenter.y + 0.2f, tileCenter.z), new Vector3(tileCenter.x + 0.2f, tileCenter.y - 0.2f, tileCenter.z), Color.red, 100); // Adjust radius as needed

    }

    void checkOverlap(Vector3 minePosition) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(minePosition, explosionRadius, layerMask);
        foreach (Collider2D collider in colliders) {
            Debug.Log("Explosion hit: " + collider.name);
            destroyTerrain(collider, minePosition);
            hitDamageable(collider, minePosition);
        }
    }

    bool hitDamageable(Collider2D collider, Vector3 minePosition) {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable == null) {
            return false;
        }

        // Hit(damageable);

        return true;
    }

    //based on "Findable_Shelf" at https://discussions.unity.com/t/remove-tiles-given-a-point-and-a-radius/756324/6
    void destroyTerrain(Collider2D collider, Vector3 minePosition) {
        Tilemap tilemap = collider.GetComponent<Tilemap>();
        if (tilemap != null) {

            int explosionRadiusCapped = Mathf.CeilToInt(explosionRadius); // expand the search area to guarantee it covers all potential surrounding tiles
            for (int x = -explosionRadiusCapped; x < explosionRadiusCapped; x++)
            {
                for (int y = -explosionRadiusCapped; y < explosionRadiusCapped; y++) //find the box
                {
                    Vector3Int tilepos = tilemap.WorldToCell(new Vector2(minePosition.x + x, minePosition.y + y));
                    Vector3 tileCenter = tilemap.GetCellCenterWorld(tilepos); // get center of specific tile that was triggered
                    if (Vector3.Distance(minePosition, tileCenter) <= explosionRadius) //check distance to make it a circle
                    {

                        tilemap.SetTile(tilepos, null);
                        Debug.DrawLine(new Vector3(tileCenter.x - 0.2f, tileCenter.y - 0.2f, tileCenter.z), new Vector3(tileCenter.x + 0.2f, tileCenter.y + 0.2f, tileCenter.z), Color.green, 100); // Adjust radius as needed

                    }
                }
            }
        }
    }

    //debug explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set explosion radius color to red

        // Loop through all tiles within the tilemap bounds
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile != null) // if there is actually a tile
                {
                    Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePosition);
                    Gizmos.DrawWireSphere(tileCenter, explosionRadius); // Adjust radius as needed
                }
            }
        }
    }
}