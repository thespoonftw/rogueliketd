using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure {

    List<Tile> occupiedTiles = new List<Tile>();

    public Structure(StructureData data, Tile originTile, int rotationIndex) {        

        var half = (Constants.BLOCK_SIZE - 1) / 2;
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int z = 0; z < Constants.BLOCK_SIZE; z++) {
                var coords = Tools.GetCoordsAfterRotationBlock(rotationIndex, x, z);
                var placementRule = data.GetPathingRule(coords.x, coords.z);
                if (placementRule == PathingRule.none) { continue; }
                var tile = GameManager.Instance.GameGrid.GetTile(x + originTile.X - half, z + originTile.Z - half);
                occupiedTiles.Add(tile);
            }
        }
        occupiedTiles.ForEach(t => t.SetOccupyingStructure(this));

        originTile.SetOccupyingStructure(this);
    }
    
}
