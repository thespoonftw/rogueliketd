using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    private BlockView[,] blockViews;
    private TileView[,] tileViews;
    private Grid grid;

    public void Init(Grid grid) {
        this.grid = grid;
        var gridSize = grid.size;
        var blockSize = Constants.BLOCK_SIZE;
        blockViews = new BlockView[gridSize, gridSize];
        tileViews = new TileView[gridSize * Constants.BLOCK_SIZE, gridSize * Constants.BLOCK_SIZE];

        var blockViewsParent = new GameObject("BlockViews");
        blockViewsParent.transform.parent = transform;
        blockViewsParent.transform.position = transform.position;
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                var go = Instantiate(Prefabs.Instance.blockViewPrefab, transform.position + GetBlockPosition(x, y), Quaternion.identity, blockViewsParent.transform);
                go.name = "Block[" + x + "," + y + "]";
                var view = go.GetComponent<BlockView>();
                view.Init(grid.GetBlock(x, y), this);
                blockViews[x, y] = view;
            }
        }

        var tileViewsParent = new GameObject("TileViews");
        tileViewsParent.transform.parent = transform;
        tileViewsParent.transform.position = transform.position;
        for (int x = 0; x < gridSize * blockSize; x++) {
            for (int y = 0; y < gridSize * blockSize; y++) {
                var go = Instantiate(Prefabs.Instance.tileViewPrefab, transform.position + GetTilePosition(x, y), Quaternion.identity, tileViewsParent.transform);
                go.name = "Tile[" + x + "," + y + "]";
                var view = go.GetComponent<TileView>();
                view.Init(grid.GetTile(x, y), this);
                tileViews[x, y] = view;
            }
        }
    }

    public Vector3 GetBlockPosition(int x, int y) {
        return new Vector3(x * Constants.BLOCK_SIZE, y * Constants.BLOCK_SIZE, 0);
    }

    public Vector3 GetTilePosition(int x, int y) {
        return new Vector3(x, y, 0);
    }
}
