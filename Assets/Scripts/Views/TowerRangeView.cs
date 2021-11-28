using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRangeView : MonoBehaviour {

    [SerializeField] LineRenderer lineRender;

    public void ShowRange(StructureData data, Tile tile, int rotationIndex) {
        
        switch (data.action) {
            case StructureAction.wall:
            case StructureAction.boulder: 
                ShowSquareRange(data, tile, rotationIndex); break;
            case StructureAction.floor: return;
            default: ShowCircularRange(data, tile); break;
        }

    }

    public void Hide() {
        lineRender.enabled = false;
    }

    public void ShowSquareRange(StructureData data, Tile tile, int rotationIndex) {
        lineRender.enabled = true;

    }

    public void ShowCircularRange(StructureData data, Tile tile) {
        lineRender.enabled = true;
        var centre = GameManager.Instance.GameGridView.GetTilePosition(tile) + new Vector3(0, 0.1f, 0);
        var range = data.range;
        var numberOfPoints = (Mathf.RoundToInt(range) * 4) + 10;
        var angle = 360f / numberOfPoints;
        lineRender.positionCount = numberOfPoints + 1;
        for (int i=0; i<=numberOfPoints; i++) {
            var pos = centre + Quaternion.Euler(0, angle * i, 0) * (range * Vector3.forward);
            lineRender.SetPosition(i, pos);
        }

    }
}
