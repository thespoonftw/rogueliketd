using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] GameObject tileChooser;

    public void ShowTileChooser() {
        tileChooser.SetActive(true);
    }

    public void TileChosen(int index) {
        PlacementTileManager.Instance.StartPlacingTile(index);
        tileChooser.SetActive(false);
    }
}
