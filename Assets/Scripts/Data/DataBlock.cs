using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataBlock : CsvDataEntry {

    private bool[,] array = new bool[Constants.BLOCK_SIZE, Constants.BLOCK_SIZE];

    public DataBlock(List<string> line) {

        var imageMap = ImageMaps.GetBlockMap(int.Parse(line[0]));

        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int y = 0; y < Constants.BLOCK_SIZE; y++) {
                var pixel = imageMap.GetPixel(x, y);
                array[x, y] = pixel == Color.white;
            }
        }
    }

    public bool IsPath(Coords coords, Direction direction) {
        var after = direction.GetCoordsAfterRotationBlock(coords);
        return array[after.x, after.z];
    }
}