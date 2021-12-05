using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DirectionValue {
    north,
    east,
    south,
    west
}

public class Direction {

    public DirectionValue Value { get; private set; }

    private int IntValue => (int)Value;

    public float YAngle => IntValue * 90;
    public Quaternion Quaternion => Quaternion.Euler(0, YAngle, 0);

    public Direction() {
        Value = DirectionValue.north;
    }

    public Direction(DirectionValue value) {
        Value = value;
    }

    public void RotateClockwise() {
        Value = (DirectionValue)((IntValue + 1) % 4);
    }

    public Direction GetOppositeDirection() {
        return new Direction((DirectionValue)((IntValue + 2) % 4));
    }


    public Coords GetCoordsAfterRotation(int x, int z, int xOrigin = 0, int zOrigin = 0) {
        var dx = x - xOrigin;
        var dz = z - zOrigin;
        switch (IntValue) {
            default: return new Coords() { x = x, z = z };
            case 1: return new Coords() { x = zOrigin - dz, z = x };
            case 2: return new Coords() { x = xOrigin - dx, z = zOrigin - dz };
            case 3: return new Coords() { x = z, z = xOrigin - dx };
        }
    }

    public Vector3 GetVectorAfterRotation(Vector3 input) {
        switch (IntValue) {
            default: return new Vector3(input.x, input.y, input.z);
            case 3: return new Vector3(-input.z, input.y, input.x);
            case 2: return new Vector3(-input.x, input.y, -input.z);
            case 1: return new Vector3(input.z, input.y, -input.x);
        }
    }

    public Coords GetCoordsAfterRotationBlock(int x, int z) {
        var mid = (Constants.BLOCK_SIZE - 1) / 2;
        return GetCoordsAfterRotation(x, z, mid, mid);
    }

    public static List<Direction> GetAll() {
        return new List<Direction>() {
            new Direction(DirectionValue.north),
            new Direction(DirectionValue.east),
            new Direction(DirectionValue.south),
            new Direction(DirectionValue.west)
        };
    }

}
