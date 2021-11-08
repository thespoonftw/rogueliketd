using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacementManager : Singleton<StructurePlacementManager>
{
    [SerializeField] GameObject structure;

    private Grid GameGrid => GameManager.Instance.GameGrid;
    private bool isPlacingStructureEnabled;
    private Tile highlightedTile;

    private void Update() {
        if (Input.GetMouseButtonUp(0) && highlightedTile != null) {
            PlaceStructureByUser(highlightedTile);
        }
    }

    public void StartPlacingStructure() {
        isPlacingStructureEnabled = true;
    }

    public void StopPlacingStructure() {
        isPlacingStructureEnabled = false;
        highlightedTile = null;
    }

    public void TryHighlightTile(Tile tile) {
        if (!isPlacingStructureEnabled) { return; }
        highlightedTile = tile;
        tile.SetHighlight(Colour.green);
    }

    public void RemoveHighlight(Tile tile) {
        tile.SetHighlight(Colour.clear);
        highlightedTile = null;
    }

    public void PlaceStructureByUser(Tile tile) {
        if (!isPlacingStructureEnabled) { return; }
        if (!tile.IsValidTilePlacement()) { return; }
        var pos = GameManager.Instance.GameGridView.GetTilePosition(tile.X, tile.Y);
        Instantiate(structure, pos, Quaternion.identity, transform);
    }
}
