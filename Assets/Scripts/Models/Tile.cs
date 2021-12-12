using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TileMode {
    noBlock,
    available,
    path,
}

public class Tile {

    public TileMode Mode { get; private set; }
    public bool IsOccupied { get { return occupyingStructure != null; } }

    public readonly Vector3 position;
    public readonly Coords coords;

    private readonly Grid grid;
    private Structure occupyingStructure;
    private Block block;
    private List<Enemy> enemies;

    public event Action<TileMode> OnTileMode;
    public event Action<Colour> OnHighlightColour;

    public Tile(Coords coords, Grid grid) {
        this.coords = coords;
        this.grid = grid;
        position = new Vector3(coords.x, 0, coords.z);
        block = grid.GetBlock(new Coords(coords.x / Constants.BLOCK_SIZE, coords.z / Constants.BLOCK_SIZE));
        enemies = new List<Enemy>();
    }

    public void SetMode(TileMode mode) {
        Mode = mode;
        OnTileMode?.Invoke(mode);
    }

    public List<Tile> GetAdjacentTiles() {
        return Direction.GetAll().Select(dir => GetAdjacentTile(dir)).ToList();
    }

    public Tile GetAdjacentTile(Direction direction) {
        return grid.GetTile(Coords.Neighbour(direction.Value));
    }

    public Tile GetNextPath(Tile previous) {
        var neighbours = GetAdjacentTiles().Where(a => a.Mode == TileMode.path).ToList();
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

    public void AddEnemy(Enemy e) {
        enemies.Add(e);
    }

    public void RemoveEnemy(Enemy e) {
        enemies.Remove(e);
    }

    public List<Enemy> GetEnemies() {
        return new List<Enemy>(enemies);
    }
}
