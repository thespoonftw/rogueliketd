using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridHitboxMode {
    Off,
    Tiles,
    Blocks,
}

public class GridView : MonoBehaviour
{
    private BlockView[,] blockViews;
    private TileView[,] tileViews;
    private Grid grid;

    public void Init(Grid grid) {
        this.grid = grid;
        var gridSize = grid.numberOfBlocks;
        var blockSize = Constants.BLOCK_SIZE;
        blockViews = new BlockView[gridSize, gridSize];
        tileViews = new TileView[gridSize * Constants.BLOCK_SIZE, gridSize * Constants.BLOCK_SIZE];

        var blockViewsParent = new GameObject("BlockViews");
        blockViewsParent.transform.parent = transform;
        blockViewsParent.transform.position = transform.position;
        for (int x = 0; x < gridSize; x++) {
            for (int z = 0; z < gridSize; z++) {
                var go = Instantiate(Prefabs.Instance.blockViewPrefab, transform.position + GetBlockPosition(x, z), Quaternion.identity, blockViewsParent.transform);
                go.name = "Block[" + x + "," + z + "]";
                var view = go.GetComponent<BlockView>();
                view.Init(grid.GetBlock(x, z), this);
                blockViews[x, z] = view;
            }
        }

        var tileViewsParent = new GameObject("TileViews");
        tileViewsParent.transform.parent = transform;
        tileViewsParent.transform.position = transform.position;
        for (int x = 0; x < gridSize * blockSize; x++) {
            for (int z = 0; z < gridSize * blockSize; z++) {
                var go = Instantiate(Prefabs.Instance.tileViewPrefab, transform.position + GetTilePosition(x, z), Quaternion.identity, tileViewsParent.transform);
                go.name = "Tile[" + x + "," + z + "]";
                var view = go.GetComponent<TileView>();
                view.Init(grid.GetTile(x, z), this);
                tileViews[x, z] = view;
            }
        }
    }

    public Vector3 GetBlockPosition(int x, int z) {
        return new Vector3(x * Constants.BLOCK_SIZE, 0, z * Constants.BLOCK_SIZE);
    }

    public Vector3 GetTilePosition(int x, int z) {
        return new Vector3(x, 0, z);
    }

    public Vector3 GetTilePosition(Tile tile) {
        return GetTilePosition(tile.X, tile.Z);
    }

    public void SetGridHitboxMode(GridHitboxMode mode) {
        foreach (var v in blockViews) {
            v.EnableHitbox(mode == GridHitboxMode.Blocks);
        }
        foreach (var v in tileViews) {
            v.EnableHitbox(mode == GridHitboxMode.Tiles);
        }
    }
}
