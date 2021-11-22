using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementManager : Singleton<BlockPlacementManager>
{
    [SerializeField] Transform blockPreview1;
    [SerializeField] Transform blockPreview2;
    [SerializeField] Transform blockPreview3;

    private Grid GameGrid => GameManager.Instance.GameGrid;
     
    private BlockData blockData;
    private int rotationIndex = 0;
    private bool isPlacingBlockEnabled = false;
    private Block highlightedBlock;

    public void Init() {
        CreatePreviewBlock(1, blockPreview1);
        CreatePreviewBlock(2, blockPreview2);
        CreatePreviewBlock(3, blockPreview3);
        Inputs.OnLeftMouseQuickRelease += TryPlaceBlockByUser;
        Inputs.OnRightMouseQuickRelease += RotateBlock;
    }

    public void TryHighlightBlock(Block block) {
        if (!isPlacingBlockEnabled) { return; }
        if (!block.IsPlaced) {
            block.PlaceTemporaryBlock(blockData, rotationIndex);
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
        blockData = BlockDataSet.GetEntry(blockIndex);
        isPlacingBlockEnabled = true;
        rotationIndex = 0;
        highlightedBlock = null;
        Raycaster.SetMode(RaycastMode.blocks);
    }

    public void RotateBlock() {
        if (highlightedBlock == null || !isPlacingBlockEnabled) { return; }
        rotationIndex = (rotationIndex + 1) % 4;
        if (!highlightedBlock.IsPlaced) { highlightedBlock.ClearBlock(); }
        TryHighlightBlock(highlightedBlock);
    }

    public void TryPlaceBlockByUser() {
        if (highlightedBlock == null) { return; }
        if (!isPlacingBlockEnabled) { return; }
        if (!highlightedBlock.IsValidBlockPlacement()) { return; }
        StopBlockPlacement();
        highlightedBlock.PlaceBlock(blockData, rotationIndex);
        CanvasManager.Instance.FinishPlacingBlock();
    }

    public void PlaceBlockWithCode(int x, int y, int blockIndex, int rotation = 0) {
        var blockData = BlockDataSet.GetEntry(blockIndex);
        var block = GameGrid.GetBlock(x, y);
        block.PlaceBlock(blockData, rotation);
    }

    public void CreatePreviewBlock(int dataIndex, Transform previewTransform) {
        var grid = new Grid(1);
        var gridgo = new GameObject("Preview " + dataIndex);
        gridgo.transform.parent = previewTransform;
        gridgo.transform.position = previewTransform.position;
        var gridView = gridgo.AddComponent<GridView>();
        gridView.Init(grid);
        var block = grid.GetBlock(0, 0);
        block.PlaceBlock(BlockDataSet.GetEntry(dataIndex), 0);
    }

    public void StopBlockPlacement() {
        isPlacingBlockEnabled = false;
        RemoveHighlight(highlightedBlock);
        Raycaster.SetMode(RaycastMode.standard);
    }
}
