using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructurePlacementManager : Singleton<StructurePlacementManager>
{
    private GameManager game;
    private bool isPlacingStructureEnabled;
    private Tile focusedTile;
    private bool isValid;
    private StructureData structureData;
    private int rotationIndex;

    [SerializeField] GameObject rotateText;

    public event Action<Tile, bool, StructureData, int> OnFocusTile;

    public void Init() {
        game = GameManager.Instance;
        Inputs.OnLeftMouseQuickRelease += UserTryPlaceStructure;
        Inputs.OnRightMouseQuickRelease += RotateStructure;
    }

    public void StartPlacingStructure(StructureData placingStructure) {
        this.structureData = placingStructure;
        isPlacingStructureEnabled = true;
        rotationIndex = 0;
        rotateText.SetActive(placingStructure.isRotatable);
        Raycaster.SetMode(RaycastMode.tiles);
    }

    public void StopPlacingStructure() {
        FocusTile(null);
        isPlacingStructureEnabled = false;
        Raycaster.SetMode(RaycastMode.standard);
    }

    public bool IsValidPlacement(Tile focusedTile, StructureData data) {
        var grid = GameManager.Instance.GameGrid;
        var half = (Constants.BLOCK_SIZE - 1) / 2;
        bool? atleastOnePath = null;
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int z = 0; z < Constants.BLOCK_SIZE; z++) {
                var coords = Tools.GetCoordsAfterRotationBlock(rotationIndex, x, z);
                var placementRule = data.GetPathingRule(coords.x, coords.z);
                if (placementRule == PathingRule.none) { continue; }
                var tileToCheck = grid.GetTile(focusedTile.X - half + x, focusedTile.Z - half + z);
                if (tileToCheck.IsOccupied) { return false; }
                if (tileToCheck.Mode == TileMode.noBlock) { return false; }
                if (placementRule == PathingRule.path && tileToCheck.Mode != TileMode.path) { return false; }
                if (placementRule == PathingRule.buildable && tileToCheck.Mode != TileMode.available) { return false; }
                if (placementRule == PathingRule.atleastOnePath && atleastOnePath != true) { atleastOnePath = tileToCheck.Mode == TileMode.path; }
            }
        }
        if (atleastOnePath == false) { return false; }
        return true;
    }

    public void FocusTile(Tile tile) {
        if (!isPlacingStructureEnabled && tile != null) { return; }
        focusedTile = tile;
        if (tile != null) { isValid = IsValidPlacement(tile, structureData); }
        OnFocusTile?.Invoke(tile, isValid, structureData, rotationIndex);        
    }

    private void RotateStructure() {
        if (!isPlacingStructureEnabled) { return; }
        if (!structureData.isRotatable) { return; }
        rotationIndex = (rotationIndex + 1) % 4;
        FocusTile(focusedTile);
    }

    private void UserTryPlaceStructure() {
        if (focusedTile == null) { return; }
        if (!isPlacingStructureEnabled) { return; }
        if (!isValid) { return; }
        if (game.Gold < structureData.cost) { return; }
        var pos = game.GameGridView.GetTilePosition(focusedTile.X, focusedTile.Z);
        var go = Instantiate(Prefabs.Instance.structurePrefab, pos, Quaternion.identity, transform);
        var view = go.GetComponent<StructureView>();
        view.Init(structureData, rotationIndex);
        var structure = new Structure(structureData, focusedTile, rotationIndex);
        game.ModifyGold(-structureData.cost);
        FocusTile(focusedTile);
    }
}
