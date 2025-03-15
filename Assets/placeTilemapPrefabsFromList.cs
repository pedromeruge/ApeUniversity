using UnityEngine;
using UnityEngine.Tilemaps;

// class to replace tiles in a Tilemap with prefabs, so each prefab is placed in a correct tile position, without having to care about tilemap properties
public class PlaceTilemapPrefabsFromList : MonoBehaviour
{
    [System.Serializable]
    public class TileToPrefab
    {
        public GameObject prefab;
        public TileBase tile;
    }

    [SerializeField] private Tilemap prefabTilemap; 
    [SerializeField] private TileToPrefab[] prefabsList; // object prefab to replace each tile with
    void Start()
    {
        ConvertItemsToPrefabs();
    }

    void ConvertItemsToPrefabs()
    {
        if (prefabTilemap == null || prefabsList == null)
        {
            Debug.LogError("ItemPlacer: Missing references to Tilemap or Prefab!");
            return;
        }

        // for all tiles in the tilemap
        BoundsInt bounds = prefabTilemap.cellBounds;
        foreach (Vector3Int tilePos in bounds.allPositionsWithin)
        {
            TileBase tile = prefabTilemap.GetTile(tilePos);
            GameObject prefab = this.getPrefabFromTile(tile); // get prefab that corresponds to that tile
            if (tile != null && prefab != null)  // only replace tiles that exist
            {
                Vector3 worldPos = prefabTilemap.GetCellCenterWorld(tilePos);

                // create prefab at the tile position
                // prefabs created as children of the tilemap itself
                Instantiate(prefab, worldPos, Quaternion.identity, this.transform);

                // remove the tile from the original tilemap
                prefabTilemap.SetTile(tilePos, null);
            }
            else if (tile != null) {
                Debug.LogWarning("ItemPlacer: Missing prefab for tile " + tile);
            }
        }
    }

    GameObject getPrefabFromTile(TileBase tile) {
        foreach (TileToPrefab tilePrefab in prefabsList)
        {
            if (tilePrefab.tile == tile)
            {
                return tilePrefab.prefab;
            }
        }
        return null;
    }
}