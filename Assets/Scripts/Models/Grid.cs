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
            for (int y=0; y<gridSize; y++) {
                blockArray[x, y] = new Block(x, y, this);
            }
        }

        for (int x=0; x< numberOfTiles; x++) {
            for (int y=0; y< numberOfTiles; y++) {
                tileArray[x, y] = new Tile(x, y, this);
            }
        }
    }

    public Block GetBlock(int x, int y) {
        if (x < 0 || y < 0 || x >= numberOfBlocks || y >= numberOfBlocks) { return null; }
        return blockArray[x, y];
    }

    public Tile GetTile(int x, int y) {
        if (x < 0 || y < 0 || x >= numberOfTiles || y >= numberOfTiles) { return null; }
        return tileArray[x, y];
    }

    public Tile GetTile(int blockX, int blockY, int tileX, int tileY) {
        return tileArray[blockX * Constants.BLOCK_SIZE + tileX, blockY * Constants.BLOCK_SIZE + tileY];
    }

    public Tile GetTile(Block block, int x, int y) {
        return GetTile(block.X, block.Z, x, y);
    }
}
