using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacementManager : Singleton<StructurePlacementManager>
{
    [SerializeField] GameObject structure;

    private Grid GameGrid => GameManager.Instance.gameGrid;
    private bool isPlacingStructureEnabled;
    private Vector2Int? highlightedPosition;

    private void Update() {
        if (Input.GetMouseButtonUp(0) && highlightedPosition != null) {
            var pos = (Vector2Int)highlightedPosition;
            PlaceStructureByUser(pos.x, pos.y);
        }
    }

    public void TryHighlightTile(int x, int y) {
        if (!isPlacingStructureEnabled) { return; }
        var tile = GameGrid.GetTile(x, y);
        tile.SetHighlight(Colour.green);
    }

    public void RemoveHighlight(int x, int y) {
        var tile = GameGrid.GetTile(x, y);
        tile.SetHighlight(Colour.clear);
    }

    public void PlaceStructureByUser(int x, int y) {
        if (!isPlacingStructureEnabled) { return; }
        var tile = GameGrid.GetTile(x, y);
        if (!tile.IsValidTilePlacement()) { return; }
        Instantiate(structure, GameGrid.Ge)
    }
}
