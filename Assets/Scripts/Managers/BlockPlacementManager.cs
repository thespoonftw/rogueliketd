using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementManager : Singleton<BlockPlacementManager>
{
    [SerializeField] Transform blockPreview1;
    [SerializeField] Transform blockPreview2;
    [SerializeField] Transform blockPreview3;

    private BlockDataLoader blockDataLoader = new BlockDataLoader();
    private Grid GameGrid => GameManager.Instance.gameGrid;
     
    private BlockData placingBlockData;
    private int rotationIndex = 0;
    private bool isPlacingBlockEnabled = false;
    private Vector2Int? highlightedPosition;

    public void Init() {
        CreatePreviewBlock(1, blockPreview1);
        CreatePreviewBlock(2, blockPreview2);
        CreatePreviewBlock(3, blockPreview3);
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1)) {
            RotateBlock();
        }
        if (Input.GetMouseButtonUp(0) && highlightedPosition != null) {
            var pos = (Vector2Int)highlightedPosition;
            PlaceBlockByUser(pos.x, pos.y);
        }
    }

    public void TryHighlightBlock(int x, int y) {
        if (!isPlacingBlockEnabled) { return; }
        var block = GameGrid.GetBlock(x, y);
        if (!block.IsPlaced) {
            block.LoadData(placingBlockData, rotationIndex);
        }
        var isValid = block.IsValidBlockPlacement();
        block.SetHighlight(isValid ? Colour.green : Colour.red);
        highlightedPosition = new Vector2Int(x, y);
    }

    public void RemoveHighlight(int x, int y) {
        var block = GameGrid.GetBlock(x, y);
        block.SetHighlight(Colour.clear);
        if (!block.IsPlaced) {
            block.ClearBlock();
        }
    }

    public void StartPlacingBlock(int blockIndex) {
        placingBlockData = blockDataLoader.GetData(blockIndex);
        isPlacingBlockEnabled = true;
        rotationIndex = 0;
        highlightedPosition = null;
    }

    public void RotateBlock() {
        if (highlightedPosition == null) { return; }
        rotationIndex = (rotationIndex + 1) % 4;
        var pos = (Vector2Int)highlightedPosition;
        var block = GameGrid.GetBlock(pos.x, pos.y);
        if (!block.IsPlaced) { block.ClearBlock(); }
        TryHighlightBlock(pos.x, pos.y);
    }

    public void PlaceBlockByUser(int x, int y) {
        if (!isPlacingBlockEnabled) { return; }
        var block = GameGrid.GetBlock(x, y);
        if (!block.IsValidBlockPlacement()) { return; }
        block.FinishPlacingBlock();
        isPlacingBlockEnabled = false;
        CanvasManager.Instance.FinishPlacingBlock();
    }

    public void PlaceBlockWithCode(int x, int y, int blockIndex, int rotation = 0) {
        var blockData = blockDataLoader.GetData(blockIndex);
        var block = GameGrid.GetBlock(x, y);
        block.LoadData(blockData, rotation);
        block.FinishPlacingBlock();
    }

    public void CreatePreviewBlock(int dataIndex, Transform previewTransform) {
        var grid = new Grid(1);
        var gridgo = new GameObject("Preview " + dataIndex);
        gridgo.transform.parent = previewTransform;
        gridgo.transform.position = previewTransform.position;
        var gridView = gridgo.AddComponent<GridView>();
        gridView.Init(grid);
        var block = grid.GetBlock(0, 0);
        block.LoadData(blockDataLoader.GetData(dataIndex), 0);
    }

    public void CancelPlacement() {
        isPlacingBlockEnabled = false;
    }
}
