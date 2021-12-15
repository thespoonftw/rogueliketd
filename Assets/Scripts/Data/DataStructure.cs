using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    swing,
    ranged,
    spread,
    bomb,
    laser,
    nova,
    cone,
}

public class DataStructure : CsvDataEntry {

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
    public readonly bool isEvenWidth;
    public readonly float rate;
    public readonly int damage;

    public DataStructure(List<string> line) {
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
        isEvenWidth = Tools.ParseBool(line[11]);
        rate = float.Parse(line[12]);
        damage = int.Parse(line[13]);

        var placementMap = ImageMaps.GetStructureMap(pathIndex);
        for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
            for (int y = 0; y < Constants.BLOCK_SIZE; y++) {
                var pixel = placementMap.GetPixel(x, y);
                placementArray[x, y] = ColorToPathing(pixel);
            }
        }
    }

    public PathingRule GetPathingRule(Coords coords) {
        return placementArray[coords.x, coords.z];
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
