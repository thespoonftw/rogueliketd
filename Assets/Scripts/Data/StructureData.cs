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
    starting,
    energy,
    simple,
    upgrade,
    single,
    duo
}

public enum StructureAction {
    beacon,
    shrine,
    wall,
    floor,
    boulder,
    ranged,
    spread,
    bomb,
    laser,
    nova,
    cone,
}


public class StructureData {

    private PathingRule[,] placementArray = new PathingRule[Constants.BLOCK_SIZE, Constants.BLOCK_SIZE];

    public readonly string name;
    public readonly StructureType type;
    public readonly int cost;
    public readonly int modelIndex;
    public readonly int pathIndex;
    public readonly StructureAction action;
    public readonly DamageType damageType;
    public readonly ResourceType requiredResource;
    public readonly ResourceType requiredResourceTwo;
    public readonly bool isRotatable;
    public readonly float range;

    public StructureData(List<string> line) {
        name = line[0];
        type = Tools.GetEnum<StructureType>(line[1]);
        cost = int.Parse(line[2]);
        modelIndex = int.Parse(line[3]);
        pathIndex = int.Parse(line[4]);
        action = Tools.GetEnum<StructureAction>(line[5]);
        damageType = Tools.GetEnum<DamageType>(line[6]);
        requiredResource = Tools.GetEnum<ResourceType>(line[7]);
        requiredResourceTwo = Tools.GetEnum<ResourceType>(line[8]);
        isRotatable = Tools.ParseBool(line[9]);
        range = float.Parse(line[10]);

        var placementMap = ImageMaps.GetStructureMap(pathIndex);
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int y = 0; y < Constants.BLOCK_SIZE; y++) {
                var pixel = placementMap.GetPixel(x, y);
                placementArray[x, y] = ColorToPathing(pixel);
            }
        }
    }

    public PathingRule GetPathingRule(int x, int z) {
        return placementArray[x, z];
    }

    public bool GetIsArea(int x, int z) {
        return placementArray[x, z] == PathingRule.buildable || placementArray[x, z] == PathingRule.path;
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
