using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    private Grid grid;
    public Tile PrevTile { get; private set; }
    public Tile NextTile { get; private set; }


    public Enemy(Grid grid, Block startBlock) {
        this.grid = grid;
        NextTile = startBlock.GetStartPath();
        MoveToNextTile();
    }

    public void MoveToNextTile() {
        var prev = PrevTile;
        PrevTile = NextTile;
        NextTile = NextTile.GetNextPath(prev);
    }
    
}
