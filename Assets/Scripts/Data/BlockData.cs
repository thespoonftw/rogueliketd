using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BlockData {

    private List<BlockDataEntry> list = new List<BlockDataEntry>();

    public BlockData() {
        var data = CsvLoader.LoadFile("Blocks");

        for (int i = 0; i < data.Count; i += 8) {
            var blockData = data.Skip(i).Take(8).ToList();
            list.Add(new BlockDataEntry(blockData));
        }
    }

    public BlockDataEntry GetData(int index) {
        return list[index];
    }
}

public class BlockDataEntry
{
    private bool[,] array = new bool[Constants.BLOCK_SIZE, Constants.BLOCK_SIZE];

    public BlockDataEntry(List<List<string>> data) {
        for (int x=0; x<7; x++) {
            for (int y=0; y<7; y++) {
                array[x, y] = (data[6 - y])[x] == "X";
            }
        }
    }

    public bool IsPath(int x, int y, int rotationIndex = 0) {
        switch (rotationIndex) {
            case 0: return array[x, y];
            case 1: return array[y, 6 - x];
            case 2: return array[6 - x, 6 - y];
            case 3: return array[6 - y, x];
            default: return false;
        }
    }

    public bool IsPathAtEdge(Direction side, int rotationIndex = 0) {
        switch (side) {
            case Direction.north: return IsPath(3, 6);
            case Direction.east: return IsPath(6, 3);
            case Direction.south: return IsPath(3, 0);
            case Direction.west: return IsPath(0, 3);
        }
        return false;
    }

}