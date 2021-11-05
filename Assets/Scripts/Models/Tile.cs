using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile {

    public int X { get; private set; }
    public int Y { get; private set; }

    public bool IsPath { get; private set; }

    private Grid grid;

    public event Action OnClear;
    public event Action OnPath;
    public event Action<Colour> OnHighlightColour;

    public Tile(int x, int y, Grid grid) {
        X = x;
        Y = y;
        this.grid = grid;
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
        return !IsPath;
    }

    public Tile GetAdjacentTile(Direction side) {
        switch (side) {
            case Direction.south: return grid.GetTile(X, Y - 1);
            case Direction.north: return grid.GetTile(X, Y + 1);
            case Direction.east: return grid.GetTile(X + 1, Y);
            case Direction.west: return grid.GetTile(X - 1, Y);
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

}
