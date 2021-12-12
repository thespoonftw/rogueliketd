using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure {

    public readonly DataStructure data;

    private List<Tile> occupiedTiles = new List<Tile>();

    public Structure(DataStructure data, Tile originTile, Direction direction) {

        var half = (Constants.BLOCK_SIZE - 1) / 2;
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int z = 0; z < Constants.BLOCK_SIZE; z++) {
                var coords = direction.GetCoordsAfterRotationBlock(new Coords(x, z));
                var placementRule = data.GetPathingRule(coords);
                if (placementRule == PathingRule.none || placementRule == PathingRule.atleastOnePath) { continue; }
                var tile = GameManager.Instance.GameGrid.GetTile(x + originTile.X - half, z + originTile.Z - half);
                occupiedTiles.Add(tile);
            }
        }
        occupiedTiles.ForEach(t => t.SetOccupyingStructure(this));

        if (data.action != StructureAction.beacon && data.action != StructureAction.shrine)
        {
            var tower = new Tower(this, data);
        }        
    }
   

}
