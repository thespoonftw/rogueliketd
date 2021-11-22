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
                blockArray[x, z] = new Block(x, z, this);
            }
        }

        for (int x=0; x< numberOfTiles; x++) {
            for (int z=0; z< numberOfTiles; z++) {
                tileArray[x, z] = new Tile(x, z, this);
            }
        }
    }

    public Block GetBlock(int x, int z) {
        if (x < 0 || z < 0 || x >= numberOfBlocks || z >= numberOfBlocks) { return null; }
        return blockArray[x, z];
    }

    public Tile GetTile(int x, int z) {
        if (x < 0 || z < 0 || x >= numberOfTiles || z >= numberOfTiles) { return null; }
        return tileArray[x, z];
    }

    public Tile GetTile(int blockX, int blockZ, int tileX, int tileZ) {
        return tileArray[blockX * Constants.BLOCK_SIZE + tileX, blockZ * Constants.BLOCK_SIZE + tileZ];
    }

    public Tile GetTile(Block block, int x, int z) {
        return GetTile(block.X, block.Z, x, z);
    }
}
