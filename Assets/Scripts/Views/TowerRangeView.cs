using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRangeView : MonoBehaviour {

    [SerializeField] LineRenderer lineRender;

    public void ShowRange(DataStructure data, Tile tile, Direction direction) {
        
        switch (data.action) {
            case StructureAction.swing:
                ShowSquareRange(data, tile, direction, 1, 1, 0); break;
            case StructureAction.wall:
                ShowSquareRange(data, tile, direction, 2, 1, 1); break;
            case StructureAction.boulder: 
                ShowSquareRange(data, tile, direction, 1, 3, 1); break;
            case StructureAction.bomb:
                ShowArcRange(data, tile, direction, Constants.BOMB_ARC); break;
            case StructureAction.floor: return;
            default: ShowCircularRange(data, tile); break;
        }

    }

    public void Hide() {
        lineRender.enabled = false; 
    }

    public void ShowSquareRange(DataStructure data, Tile tile, Direction direction, int width, int length, int offset) {
        lineRender.enabled = true;
        lineRender.positionCount = 5;
        var centre = tile.position + new Vector3(0, 0.1f, 0);
        var v1 = direction.GetVectorAfterRotation(new Vector3(-0.5f + width, 0, 0.5f - offset));
        var v2 = direction.GetVectorAfterRotation(new Vector3(-0.5f + width, 0, 0.5f - offset - length));
        var v3 = direction.GetVectorAfterRotation(new Vector3(-0.5f,         0, 0.5f - offset - length));
        var v4 = direction.GetVectorAfterRotation(new Vector3(-0.5f,         0, 0.5f - offset));
        lineRender.SetPosition(0, centre + v1);
        lineRender.SetPosition(1, centre + v2);
        lineRender.SetPosition(2, centre + v3);
        lineRender.SetPosition(3, centre + v4);
        lineRender.SetPosition(4, centre + v1);

    }

    public void ShowCircularRange(DataStructure data, Tile tile) {
        lineRender.enabled = true;
        var offset = data.isEvenWidth ? new Vector3(0.5f, 0.1f, 0.5f) : new Vector3(0, 0.1f, 0);
        var centre = tile.position + offset;
        var numberOfPoints = GetNumberOfPointsForRange(data.range);
        var angleBetweenPoints = 360f / numberOfPoints;
        lineRender.positionCount = numberOfPoints + 1;
        for (int i=0; i<=numberOfPoints; i++) {
            var pos = centre + Quaternion.Euler(0, angleBetweenPoints * i, 0) * (data.range * Vector3.forward);
            lineRender.SetPosition(i, pos);
        }
    }

    public void ShowArcRange(DataStructure data, Tile tile, Direction direction, float angle) {
        lineRender.enabled = true;
        var offset = data.isEvenWidth ? direction.GetVectorAfterRotation(new Vector3(0.5f, 0.1f, 0.5f)) : new Vector3(0, 0.1f, 0);
        var centre = tile.position + offset;
        var range = data.range;
        var numberOfPoints = Mathf.RoundToInt(GetNumberOfPointsForRange(range) * (angle / 360));
        var angleBetweenPoints = angle / numberOfPoints;
        lineRender.positionCount = numberOfPoints + 2;
        lineRender.SetPosition(0, centre);
        for (int i = 0; i < numberOfPoints; i++) {
            var half = numberOfPoints / 2f;
            var yAngle = (angleBetweenPoints * (i - half)) + direction.YAngle;            
            var pos = centre + Quaternion.Euler(0, yAngle, 0) * (data.range * Vector3.back);
            lineRender.SetPosition(i + 1, pos);
        }
        lineRender.SetPosition(numberOfPoints + 1, centre);
    }

    private int GetNumberOfPointsForRange(float range) {
        return (Mathf.RoundToInt(range) * 4) + 10;
    }
}
