using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementManager : Singleton<BlockPlacementManager>
{
    [SerializeField] Transform blockPreview1;
    [SerializeField] Transform blockPreview2;
    [SerializeField] Transform blockPreview3;

    private Grid GameGrid => GameManager.Instance.GameGrid;
     
    private DataBlock blockData;
    private Direction currentDirection;
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
            block.PlaceTemporaryBlock(blockData, currentDirection);
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
        blockData = Data.Blocks.GetEntry(blockIndex);
        isPlacingBlockEnabled = true;
        currentDirection = new Direction();
        highlightedBlock = null;
        Raycaster.SetMode(RaycastMode.blocks);
    }

    public void RotateBlock() {
        if (highlightedBlock == null || !isPlacingBlockEnabled) { return; }
        currentDirection.RotateClockwise();
        if (!highlightedBlock.IsPlaced) { highlightedBlock.ClearBlock(); }
        TryHighlightBlock(highlightedBlock);
    }

    public void TryPlaceBlockByUser() {
        if (highlightedBlock == null) { return; }
        if (!isPlacingBlockEnabled) { return; }
        if (!highlightedBlock.IsValidBlockPlacement()) { return; }
        StopBlockPlacement();
        highlightedBlock.PlaceBlock(blockData, currentDirection);
        CanvasManager.Instance.FinishPlacingBlock();
    }

    public void PlaceBlockWithCode(Coords blockCoords, int blockIndex, Direction direction) {
        var blockData = Data.Blocks.GetEntry(blockIndex);
        var block = GameGrid.GetBlock(blockCoords);
        block.PlaceBlock(blockData, direction);
    }

    public void CreatePreviewBlock(int dataIndex, Transform previewTransform) {
        var grid = new Grid(1);
        var gridgo = new GameObject("Preview " + dataIndex);
        gridgo.transform.parent = previewTransform;
        gridgo.transform.position = previewTransform.position;
        var gridView = gridgo.AddComponent<GridView>();
        gridView.Init(grid);
        var block = grid.GetBlock(new Coords(0, 0));
        block.PlaceBlock(Data.Blocks.GetEntry(dataIndex), new Direction());
    }

    public void StopBlockPlacement() {
        isPlacingBlockEnabled = false;
        RemoveHighlight(highlightedBlock);
        Raycaster.SetMode(RaycastMode.standard);
    }
}
