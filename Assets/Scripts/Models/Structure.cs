using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure {

    Tile occupiedTile;

    public Structure(Tile tile) {
        this.occupiedTile = tile;
        tile.SetOccupyingStructure(this);
    }
    
}
