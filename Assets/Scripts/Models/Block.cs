using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block {

    public BlockDataEntry data;
    public readonly int X;
    public readonly int Z;
    private readonly Grid grid;

    public event Action<Colour> OnHighlightColour;
    public event Action OnIsPlaced;
    public event Action OnClear;

    public bool IsPlaced { get; private set; }    

    public Block(int x, int z, Grid grid) {
        this.X = x;
        this.Z = z;
        this.grid = grid;
    }

    public void LoadData(BlockDataEntry data, int rotationIndex) {
        for (int x = 0; x < 7; x++) {
            for (int y = 0; y < 7; y++) {
                if (data.IsPath(x, y, rotationIndex)) {
                    grid.GetTile(X, Z, x, y).CreatePath();
                }
            }
        }
    }

    public void FinishPlacingBlock() {
        IsPlaced = true;
        OnIsPlaced?.Invoke();
    }

    public void ClearBlock() {
        OnClear?.Invoke();
        GetAllTiles().ForEach(t => t.ClearTile());
    }

    public Tile GetTileAtEdge(Direction side) {
        switch (side) {
            case Direction.south: return grid.GetTile(this, 3, 0);
            case Direction.west: return grid.GetTile(this, 0, 3);
            case Direction.north: return grid.GetTile(this, 3, 6);
            case Direction.east: return grid.GetTile(this, 6, 3);
        }
        return null;
    }

    public Tile GetStartPath() {
        return grid.GetTile(this, 3, 3);
    }

    public Block GetAdjacentBlock(Direction side) {
        switch (side) {
            case Direction.south: return grid.GetBlock(X, Z - 1);
            case Direction.north: return grid.GetBlock(X, Z + 1);
            case Direction.east: return grid.GetBlock(X + 1, Z);
            case Direction.west: return grid.GetBlock(X - 1, Z);
        }
        return null;
    }

    public bool IsValidBlockPlacement() {
        if (IsPlaced) { return false; }
        var noNeighbours = Constants.ALL_DIRECTIONS.All(dir => GetAdjacentBlock(dir) != null && !GetAdjacentBlock(dir).IsPlaced);
        if (noNeighbours) { return false; }
        foreach (var dir in Constants.ALL_DIRECTIONS) {
            var adj = GetAdjacentBlock(dir);
            if (adj == null) { return false; }
            var t1 = GetTileAtEdge(dir);
            var t2 = adj.GetTileAtEdge(Tools.GetOppositeSide(dir));
            if (adj.IsPlaced && GetTileAtEdge(dir).IsPath != adj.GetTileAtEdge(Tools.GetOppositeSide(dir)).IsPath) { return false; }
        }
        return true;
    }

    public void SetHighlight(Colour highlightColour) {
        OnHighlightColour?.Invoke(highlightColour);
    }

    public List<Tile> GetAllTiles() {
        var returner = new List<Tile>();
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int y = 0; y < Constants.BLOCK_SIZE; y++) {
                returner.Add(grid.GetTile(this, x, y));
            }
        }

        return returner;
    }




}
