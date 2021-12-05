using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block {

    public DataBlock data;
    public readonly int X;
    public readonly int Z;
    private readonly Grid grid;

    public bool IsPlaced { get; private set; }

    public event Action<Colour> OnHighlightColour; 

    public Block(int x, int z, Grid grid) {
        this.X = x;
        this.Z = z;
        this.grid = grid;
    }

    private void SetPathTiles(DataBlock data, Direction direction) {
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int y = 0; y < Constants.BLOCK_SIZE; y++) {
                if (data.IsPath(x, y, direction)) {
                    grid.GetTile(X, Z, x, y).SetMode(TileMode.path);
                }
            }
        }
    }

    public void PlaceTemporaryBlock(DataBlock data, Direction direction) {
        IsPlaced = false;
        GetAllTiles().ForEach(t => t.SetMode(TileMode.available));
        SetPathTiles(data, direction);
    }

    public void PlaceBlock(DataBlock data, Direction direction) {
        IsPlaced = true;
        GetAllTiles().ForEach(t => t.SetMode(TileMode.available));
        SetPathTiles(data, direction);
    }

    public void ClearBlock() {
        IsPlaced = false;
        GetAllTiles().ForEach(t => t.SetMode(TileMode.noBlock));
    }

    public Tile GetTileAtEdge(Direction direction) {
        switch (direction.Value) {
            case DirectionValue.south: return grid.GetTile(this, 3, 0);
            case DirectionValue.west: return grid.GetTile(this, 0, 3);
            case DirectionValue.north: return grid.GetTile(this, 3, 6);
            case DirectionValue.east: return grid.GetTile(this, 6, 3);
        }
        return null;
    }

    public Tile GetStartPath() {
        return grid.GetTile(this, 3, 3);
    }

    public Block GetAdjacentBlock(Direction direction) {
        switch (direction.Value) {
            case DirectionValue.south: return grid.GetBlock(X, Z - 1);
            case DirectionValue.north: return grid.GetBlock(X, Z + 1);
            case DirectionValue.east: return grid.GetBlock(X + 1, Z);
            case DirectionValue.west: return grid.GetBlock(X - 1, Z);
        }
        return null;
    }

    public bool IsValidBlockPlacement() {
        if (IsPlaced) { return false; }
        var noNeighbours = Direction.GetAll().All(dir => GetAdjacentBlock(dir) != null && !GetAdjacentBlock(dir).IsPlaced);
        if (noNeighbours) { return false; }
        foreach (var dir in Direction.GetAll()) {
            var adj = GetAdjacentBlock(dir);
            if (adj == null) { return false; }
            var t1 = GetTileAtEdge(dir);
            var t2 = adj.GetTileAtEdge(dir.GetOppositeDirection());
            if (adj.IsPlaced && GetTileAtEdge(dir).Mode != adj.GetTileAtEdge(dir.GetOppositeDirection()).Mode) { return false; }
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
