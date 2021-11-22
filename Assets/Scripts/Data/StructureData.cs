using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDataSet {

    private static List<StructureData> list = new List<StructureData>();

    public static void Load() {
        var data = CsvLoader.LoadFile("Structures");
        data.ForEach(d => list.Add(new StructureData(d)));

    }

    public static StructureData GetEntry(int index) {
        return list[index];
    }
}

public enum PathingRule {
    none,               // white
    buildable,          // black
    path,               // green
    atleastOnePath,     // red
}

public enum StructureType {
    inactive,           // 0
    towerStandard,      // 1
    towerMelee,          // 2
    towerNova,          // 3
}


public class StructureData {

    private PathingRule[,] placementArray = new PathingRule[Constants.BLOCK_SIZE, Constants.BLOCK_SIZE];
    private bool[,] areaArray = new bool[Constants.MAX_STRUCTURE_AREA, Constants.MAX_STRUCTURE_AREA];

    public readonly string name;
    public readonly int cost;
    public readonly int modelIndex;
    public readonly StructureType type;

    public StructureData(List<string> line) {
        name = line[0];
        cost = int.Parse(line[1]);
        modelIndex = int.Parse(line[2]);
        type = (StructureType)int.Parse(line[3]);
        var placementMap = ImageMaps.GetStructurePlacementMap(modelIndex);
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int y = 0; y < Constants.BLOCK_SIZE; y++) {
                var pixel = placementMap.GetPixel(x, y);
                placementArray[x, y] = ColorToPathing(pixel);
            }
        }
        var areaMap = ImageMaps.GetStructureAreaMap(modelIndex);
        for (int x = 0; x < Constants.MAX_STRUCTURE_AREA; x++) {
            for (int y = 0; y < Constants.MAX_STRUCTURE_AREA; y++) {
                var pixel = areaMap.GetPixel(x, y);
                areaArray[x, y] = pixel == Color.black;
            }
        }
    }

    public PathingRule GetPathingRule(int x, int z) {
        return placementArray[x, z];
    }

    public bool GetIsArea(int x, int z) {
        return areaArray[x, z];
    }

    private PathingRule ColorToPathing(Color color) {
        if (color == Color.red) {
            return PathingRule.atleastOnePath;
        } else if (color == Color.black) {
            return PathingRule.buildable;
        } else if (color == Color.green) {
            return PathingRule.path;
        } else {
            return PathingRule.none;
        }
    }
    
}
