using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileWithPrefab", menuName = "Tiles/TileWithPrefab")]
public class MineTileWithObject : Tile
{
    public GameObject prefab; // associate prefab with tile

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.gameObject = prefab;
    }
}