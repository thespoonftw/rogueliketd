using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure {

    public readonly StructureData data;

    private List<Tile> occupiedTiles = new List<Tile>();
    private List<Tile> areaOfEffect = new List<Tile>();

    public Structure(StructureData data, Tile originTile, int rotationIndex) {        

        var half = (Constants.BLOCK_SIZE - 1) / 2;
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int z = 0; z < Constants.BLOCK_SIZE; z++) {
                var coords = Tools.GetCoordsAfterRotationBlock(rotationIndex, x, z);
                var placementRule = data.GetPathingRule(coords.x, coords.z);
                if (placementRule == PathingRule.none || placementRule == PathingRule.atleastOnePath) { continue; }
                var tile = GameManager.Instance.GameGrid.GetTile(x + originTile.X - half, z + originTile.Z - half);
                occupiedTiles.Add(tile);
            }
        }
        occupiedTiles.ForEach(t => t.SetOccupyingStructure(this));

        if (data.action != StructureAction.beacon && data.action != StructureAction.shrine)
        {
            var tower = new Tower(this);
        }        
    }
    
    public List<Tile> GetAreaOfEffect() {
        return new List<Tile>(areaOfEffect);
    }

}
