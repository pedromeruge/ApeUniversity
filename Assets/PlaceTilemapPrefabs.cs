using UnityEngine;
using UnityEngine.Tilemaps;

// class to replace tiles in a Tilemap with prefabs, so each prefab is placed in a correct tile position, without having to care about tilemap properties
public class PlaceItemPrefabs : MonoBehaviour
{
    [SerializeField] private Tilemap prefabTilemap; 
    [SerializeField] private GameObject prefab; // object prefab to replace each tile with
    void Start()
    {
        ConvertMinesToPrefabs();
    }

    void ConvertMinesToPrefabs()
    {
        if (prefabTilemap == null || prefab == null)
        {
            Debug.LogError("MinePlacer: Missing references to Tilemap or Prefab!");
            return;
        }

        // for all tiles in the tilemap
        BoundsInt bounds = prefabTilemap.cellBounds;
        foreach (Vector3Int tilePos in bounds.allPositionsWithin)
        {
            TileBase tile = prefabTilemap.GetTile(tilePos);
            if (tile != null)  // only replace tiles that exist
            {
                Vector3 worldPos = prefabTilemap.GetCellCenterWorld(tilePos);

                // create prefab at the tile position
                // prefabs created as children of the tilemap itself
                Instantiate(prefab, worldPos, Quaternion.identity, this.transform);

                // remove the tile from the original tilemap
                prefabTilemap.SetTile(tilePos, null);
            }
        }
    }
}