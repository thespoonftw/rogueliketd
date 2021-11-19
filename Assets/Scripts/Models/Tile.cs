using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile {

    public int X { get; private set; }
    public int Z { get; private set; }

    public bool IsPath { get; private set; }

    private readonly Grid grid;
    private Structure occupyingStructure;
    private Block block;

    public event Action OnClear;
    public event Action OnPath;
    public event Action<Colour> OnHighlightColour;

    public Tile(int x, int y, Grid grid) {
        X = x;
        Z = y;
        this.grid = grid;
        block = grid.GetBlock(x / Constants.BLOCK_SIZE, y / Constants.BLOCK_SIZE);
    }

    public void CreatePath() {
        IsPath = true;
        OnPath?.Invoke();
    }

    public void ClearTile() {
        IsPath = false;
        OnClear?.Invoke();
    }

    public List<Tile> GetAdjacentTiles() {
        return Constants.ALL_DIRECTIONS.Select(dir => GetAdjacentTile(dir)).ToList();
    }

    public bool IsValidTilePlacement() {
        return !IsPath && occupyingStructure == null && block.IsPlaced;
    }

    public Tile GetAdjacentTile(Direction side) {
        switch (side) {
            case Direction.south: return grid.GetTile(X, Z - 1);
            case Direction.north: return grid.GetTile(X, Z + 1);
            case Direction.east: return grid.GetTile(X + 1, Z);
            case Direction.west: return grid.GetTile(X - 1, Z);
        }
        return null;
    }

    public Tile GetNextPath(Tile previous) {
        var neighbours = GetAdjacentTiles().Where(a => a.IsPath).ToList();
        if (neighbours.Count == 0) { return null; }
        else if (neighbours.Count == 1 && previous == null) { return neighbours[0]; }
        else if (neighbours.Count == 2 && neighbours[0] == previous) { return neighbours[1]; }
        else if (neighbours.Count == 2 && neighbours[1] == previous) { return neighbours[0]; }
        else { return null; }
    }

    public void SetHighlight(Colour highlightColour) {
        OnHighlightColour?.Invoke(highlightColour);
    }

    public void SetOccupyingStructure(Structure s) {
        occupyingStructure = s;
    }

}
