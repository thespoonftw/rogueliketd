using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public readonly int numberOfBlocks;
    public readonly int numberOfTiles;
    private Block[,] blockArray;
    private Tile[,] tileArray;

    public Grid(int gridSize) {
        this.numberOfBlocks = gridSize;
        this.numberOfTiles = gridSize * Constants.BLOCK_SIZE;
        blockArray = new Block[numberOfBlocks, numberOfBlocks];
        tileArray = new Tile[numberOfTiles, numberOfTiles];

        for (int x=0; x<gridSize; x++) {
            for (int z=0; z<gridSize; z++) {
                blockArray[x, z] = new Block(new Coords(x, z), this);
            }
        }

        for (int x=0; x< numberOfTiles; x++) {
            for (int z=0; z< numberOfTiles; z++) {
                tileArray[x, z] = new Tile(new Coords(x, z), this);
            }
        }
    }

    public Block GetStartBlock() {
        var centre = (Constants.GAME_GRID_SIZE - 1) / 2;
        return GetBlock(new Coords(centre, centre));
    }

    public Block GetBlock(Coords blockCoords) {
        if (blockCoords.x < 0 || blockCoords.z < 0 || blockCoords.x >= numberOfBlocks || blockCoords.z >= numberOfBlocks) { return null; }
        return blockArray[blockCoords.x, blockCoords.z];
    }

    public Tile GetTile(int x, int z) => GetTile(new Coords(x, z));

    public Tile GetTile(Coords coords) {
        if (coords.x < 0 || coords.z < 0 || coords.x >= numberOfTiles || coords.z >= numberOfTiles) { return null; }
        return tileArray[coords.x, coords.z];
    }

    public Tile GetTile(Block block, Coords tileCoords) {
        return GetTile(block.blockCoords, tileCoords);
    }

    public Tile GetTile(Coords blockCoords, Coords tileCoords) {
        return tileArray[blockCoords.x * Constants.BLOCK_SIZE + tileCoords.x, blockCoords.z * Constants.BLOCK_SIZE + tileCoords.z];
    }
}
