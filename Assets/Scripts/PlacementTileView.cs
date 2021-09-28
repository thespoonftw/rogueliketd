using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTileView : MonoBehaviour
{
    private int x;
    private int y;
    private PlacementTileManager tileManager;

    public void Initialise(int x, int y) {
        this.x = x;
        this.y = y;
        tileManager = PlacementTileManager.Instance;
    }

    public void SetColour(bool isGreen) {
        GetComponent<SpriteRenderer>().color = isGreen ? new Color(0, 1, 0, 0.5f) : Color.clear;
    }

    private void OnMouseEnter() {
        tileManager.TryHighlightPlacementTile(x, y);
    }

    private void OnMouseExit() {
        tileManager.UnHighlightPlacementTile(x, y);
    }
}
