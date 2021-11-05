using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveller {

    private Grid grid;
    private Tile prevTile;
    public Tile CurrentTile { get; private set; }

    public event Action OnTileUpdate;


    public Traveller(Grid grid) {
        this.grid = grid;
        CurrentTile = grid.GetBlock(4, 4).GetStartPath();
    }

    public void Tick() {
        var prev = prevTile;
        prevTile = CurrentTile;
        SetPath(CurrentTile.GetNextPath(prev));
    }

    private void SetPath(Tile tile) {
        CurrentTile = tile;
        OnTileUpdate?.Invoke();
    }
    
}
