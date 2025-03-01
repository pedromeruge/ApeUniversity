using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.Collections;
public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private float countdown = 2.0f;
    [SerializeField] private float explosionRadius = 1.0f;

    [SerializeField] LayerMask layerMask;

    [SerializeField] AnimationCurve flashAnim;

    [ColorUsage(true, true)]
    [SerializeField] Color flashColor;

    [SerializeField] GameObject explosionFxPrefab;

    [SerializeField] float cameraShakeDuration = 1.2f;
    [SerializeField] float cameraShakeStrength = 10.0f;
    private Renderer spriteRenderer;

    void Awake() {

        spriteRenderer = GetComponent<Renderer>();
        if (spriteRenderer == null) {
            Debug.LogError("Mine sprite not found");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name + " entered explosion");
        StartCoroutine(countdownExplosion(countdown));
    }

    IEnumerator countdownExplosion(float seconds) {
        StartCoroutine(SpriteEffects.ColorFlasher(spriteRenderer, flashAnim, flashColor, seconds));
        yield return new WaitForSeconds(seconds);

        Vector3 tileCenter = this.transform.position;

        playExplosionFx(tileCenter);
        checkOverlapAndDestroy(tileCenter);
        destroySelf(tileCenter);

        Debug.DrawLine(new Vector3(tileCenter.x - 0.2f, tileCenter.y + 0.2f, tileCenter.z), new Vector3(tileCenter.x + 0.2f, tileCenter.y - 0.2f, tileCenter.z), Color.red, 100); // Adjust radius as needed
    }

    // play the explosion FX that appears in front of the bomb
    void playExplosionFx(Vector3 minePosition) {
        GameObject explosionFxInstance = Instantiate(explosionFxPrefab, minePosition + explosionFxPrefab.transform.position, Quaternion.identity); // load explosion prefab // add prefab position to set correct z value
        CameraShake.Shake(cameraShakeDuration, cameraShakeStrength); // shake camera
        Animator animator = explosionFxInstance.GetComponent<Animator>();
        float explosionDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosionFxInstance, explosionDuration); // destroy object after animation finishes
    }

    void checkOverlapAndDestroy(Vector3 minePosition) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(minePosition, explosionRadius, layerMask);
        foreach (Collider2D collider in colliders) {
            Debug.Log("Explosion hit: " + collider.name);
            destroyTerrain(collider, minePosition);
            hitDamageable(collider, minePosition);
        }
    }

    void destroySelf(Vector3 minePosition) {
        // destroy the prefab
        Destroy(this.gameObject); 
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
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Adjust radius as needed
    }
}