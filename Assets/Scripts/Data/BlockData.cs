using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BlockDataSet {

    private static List<BlockData> list = new List<BlockData>();

    public static void Load() {
        var data = CsvLoader.LoadFile("Blocks");
        data.ForEach(d => list.Add(new BlockData(d)));
    }

    public static BlockData GetEntry(int index) {
        return list[index];
    }
}

public class BlockData {

    private bool[,] array = new bool[Constants.BLOCK_SIZE, Constants.BLOCK_SIZE];

    public BlockData(List<string> line) {

        var imageMap = ImageMaps.GetBlockMap(int.Parse(line[0]));

        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int y = 0; y < Constants.BLOCK_SIZE; y++) {
                var pixel = imageMap.GetPixel(x, y);
                array[x, y] = pixel == Color.white;
            }
        }
    }

    public bool IsPath(int x, int z, int rotationIndex = 0) {
        var coords = Tools.GetCoordsAfterRotationBlock(rotationIndex, x, z);
        return array[coords.x, coords.z];
    }
}