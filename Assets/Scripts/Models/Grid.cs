using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public readonly int size;
    private Block[,] blockArray;
    private Tile[,] tileArray;

    public Grid(int gridSize) {
        this.size = gridSize;
        var blockSize = Constants.BLOCK_SIZE;
        blockArray = new Block[gridSize, gridSize];
        tileArray = new Tile[gridSize * blockSize, gridSize * blockSize];

        for (int x=0; x<gridSize; x++) {
            for (int y=0; y<gridSize; y++) {
                blockArray[x, y] = new Block(x, y, this);
            }
        }

        for (int x=0; x<gridSize*blockSize; x++) {
            for (int y=0; y<gridSize*blockSize; y++) {
                tileArray[x, y] = new Tile(x, y, this);
            }
        }
    }

    public Block GetBlock(int x, int y) {
        return blockArray[x, y];
    }

    public Tile GetTile(int x, int y) {
        return tileArray[x, y];
    }

    public Tile GetTile(int blockX, int blockY, int tileX, int tileY) {
        return tileArray[blockX * Constants.BLOCK_SIZE + tileX, blockY * Constants.BLOCK_SIZE + tileY];
    }

    public Tile GetTile(Block block, int x, int y) {
        return GetTile(block.X, block.Y, x, y);
    }
}
