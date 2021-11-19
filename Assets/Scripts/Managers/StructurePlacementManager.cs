using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacementManager : Singleton<StructurePlacementManager>
{
    [SerializeField] GameObject structurePrefab;

    private GameManager game;
    private bool isPlacingStructureEnabled;
    private Tile highlightedTile;

    private StructureDataEntry placingStructure;

    public void Init() {
        game = GameManager.Instance;
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0) && highlightedTile != null) {
            UserTryPlaceStructure();
        }
    }

    public void StartPlacingStructure(StructureDataEntry placingStructure) {
        this.placingStructure = placingStructure;
        isPlacingStructureEnabled = true;
        game.GameGridView.SetGridHitboxMode(GridHitboxMode.Tiles);
    }

    public void StopPlacingStructure() {
        RemoveHighlight(highlightedTile);
        isPlacingStructureEnabled = false;
        highlightedTile = null;
        game.GameGridView.SetGridHitboxMode(GridHitboxMode.Off);
    }

    public void TryHighlightTile(Tile tile) {
        if (!isPlacingStructureEnabled) { return; }
        highlightedTile = tile;
        var isValid = tile.IsValidTilePlacement();
        tile.SetHighlight(isValid ? Colour.green : Colour.red);
    }

    public void RemoveHighlight(Tile tile) {
        if (tile == null) { return; }
        tile.SetHighlight(Colour.clear);
        highlightedTile = null;
    }

    public void UserTryPlaceStructure() {
        var tile = highlightedTile;
        if (!isPlacingStructureEnabled) { return; }
        if (!tile.IsValidTilePlacement()) { return; }
        if (game.Gold < placingStructure.cost) { return; }
        var pos = game.GameGridView.GetTilePosition(tile.X, tile.Z);
        var go = Instantiate(structurePrefab, pos, Quaternion.identity, transform);
        var structure = new Structure(tile);
        game.ModifyGold(-placingStructure.cost);
        TryHighlightTile(tile);
    }
}
