using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementManager : Singleton<BlockPlacementManager>
{
    [SerializeField] Transform blockPreview1;
    [SerializeField] Transform blockPreview2;
    [SerializeField] Transform blockPreview3;

    private BlockData blockDataLoader = new BlockData();
    private Grid GameGrid => GameManager.Instance.GameGrid;
     
    private BlockDataEntry placingBlockData;
    private int rotationIndex = 0;
    private bool isPlacingBlockEnabled = false;
    private Block highlightedBlock;

    public void Init() {
        CreatePreviewBlock(1, blockPreview1);
        CreatePreviewBlock(2, blockPreview2);
        CreatePreviewBlock(3, blockPreview3);
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1)) {
            RotateBlock();
        }
        if (Input.GetMouseButtonUp(0) && highlightedBlock != null) {
            PlaceBlockByUser(highlightedBlock);
        }
    }

    public void TryHighlightBlock(Block block) {
        if (!isPlacingBlockEnabled) { return; }
        if (!block.IsPlaced) {
            block.LoadData(placingBlockData, rotationIndex);
        }
        var isValid = block.IsValidBlockPlacement();
        block.SetHighlight(isValid ? Colour.green : Colour.red);
        highlightedBlock = block;
    }

    public void RemoveHighlight(Block block) {
        if (block == null) { return; }
        block.SetHighlight(Colour.clear);
        if (!block.IsPlaced) {
            block.ClearBlock();
        }
    }

    public void StartPlacingBlock(int blockIndex) {
        placingBlockData = blockDataLoader.GetData(blockIndex);
        isPlacingBlockEnabled = true;
        rotationIndex = 0;
        highlightedBlock = null;
        GameManager.Instance.GameGridView.SetGridHitboxMode(GridHitboxMode.Blocks);
    }

    public void RotateBlock() {
        if (highlightedBlock == null) { return; }
        rotationIndex = (rotationIndex + 1) % 4;
        if (!highlightedBlock.IsPlaced) { highlightedBlock.ClearBlock(); }
        TryHighlightBlock(highlightedBlock);
    }

    public void PlaceBlockByUser(Block block) {
        if (!isPlacingBlockEnabled) { return; }
        if (!block.IsValidBlockPlacement()) { return; }
        block.FinishPlacingBlock();
        StopBlockPlacement();
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

    public void StopBlockPlacement() {
        isPlacingBlockEnabled = false;
        RemoveHighlight(highlightedBlock);
        GameManager.Instance.GameGridView.SetGridHitboxMode(GridHitboxMode.Off);
    }
}
