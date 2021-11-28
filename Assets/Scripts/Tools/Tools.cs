using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    north,
    east,
    south,
    west
}

public enum Colour {
    red,
    green,
    clear,
    white,
}

public struct Coords {
    public int x;
    public int z;
}


public class Tools : MonoBehaviour
{   
    public static Direction GetOppositeSide(Direction side) {
        return (Direction)((((int)side) + 2) % 4);
    }

    public static Quaternion GetRotation(int index) {
        return Quaternion.Euler(0, index * 90, 0);
    }

    public static Coords GetCoordsAfterRotation(int rotationIndex, int x, int z, int xOrigin = 0, int zOrigin = 0) {
        var dx = x - xOrigin;
        var dz = z - zOrigin;
        switch (rotationIndex) {
            default: return new Coords() { x = x, z = z };
            case 1: return new Coords() { x = zOrigin - dz, z = x };
            case 2: return new Coords() { x = xOrigin - dx, z = zOrigin - dz };
            case 3: return new Coords() { x = z, z = xOrigin - dx };
        }
    }

    public static Coords GetCoordsAfterRotationBlock(int rotationIndex, int x, int z) {
        var mid = (Constants.BLOCK_SIZE - 1) / 2;
        return GetCoordsAfterRotation(rotationIndex, x, z, mid, mid);
    }

    public static T GetEnum<T>(string s) where T : struct {
        Enum.TryParse(s, out T returner);
        return returner;
    }

    public static bool ParseBool(string s) {
        return s == "TRUE";
    }
}
