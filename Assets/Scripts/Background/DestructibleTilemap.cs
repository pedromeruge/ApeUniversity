using UnityEngine;
using UnityEngine.Tilemaps;

// flag a tilemap as destructible by explosives
public class DestructibleTilemap : MonoBehaviour, IDestructible
{
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
    public void Destroy(Vector3 destroyPos, float destroyRadius)
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        if (tilemap != null) {
            int destroyRadiusCapped = Mathf.CeilToInt(destroyRadius); // expand the search area to guarantee it covers all potential surrounding tiles

            for (int x = -destroyRadiusCapped; x < destroyRadiusCapped; x++)
            {
                for (int y = -destroyRadiusCapped; y < destroyRadiusCapped; y++) //find the box
                {
                    Vector3Int tilePosWorld = tilemap.WorldToCell(new Vector2(destroyPos.x + x, destroyPos.y + y));
                    Vector3 tilePosCenter = tilemap.GetCellCenterWorld(tilePosWorld); // get center of specific tile that is being exploded

                    if (Vector2.Distance(destroyPos, tilePosCenter) <= destroyRadius) //check distance to make it a circle
                    {
                        tilemap.SetTile(tilePosWorld, null);
                        Debug.DrawLine(new Vector3(tilePosCenter.x - 0.2f, tilePosCenter.y - 0.2f, tilePosCenter.z), new Vector3(tilePosCenter.x + 0.2f, tilePosCenter.y + 0.2f, tilePosCenter.z), Color.green, 100); // Adjust radius as needed

                    }
                }
            }
        }
    }
}

