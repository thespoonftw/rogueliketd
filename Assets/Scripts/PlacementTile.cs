using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementTileColor
{
    red,
    green,
    clear,
}

public class PlacementTile : MonoBehaviour
{
    private int x;
    private int y;
    private PlacementTileManager tileManager;

    public void Initialise(int x, int y) {
        this.x = x;
        this.y = y;
        tileManager = PlacementTileManager.Instance;
    }

    public void SetColour(PlacementTileColor color) {
        switch (color)
        {
            case PlacementTileColor.clear: GetComponent<SpriteRenderer>().color = Color.clear; break;
            case PlacementTileColor.green: GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f); break;
            case PlacementTileColor.red: GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f); break;
        }
    }

    private void OnMouseEnter() {
        tileManager.TryHighlightPlacementTile(x, y);
    }

    private void OnMouseExit() {
        tileManager.UnHighlightPlacementTile(x, y);
    }
}
