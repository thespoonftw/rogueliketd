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

public struct Direction {

    public DirectionValue Value { get; private set; }

    private int IntValue => (int)Value;

    public float YAngle => IntValue * 90;
    public Quaternion Quaternion => Quaternion.Euler(0, YAngle, 0);

    public Direction(DirectionValue value) {
        Value = value;
    }

    public void RotateClockwise() {
        Value = (DirectionValue)((IntValue + 1) % 4);
    }

    public Direction GetOppositeDirection() {
        return new Direction((DirectionValue)((IntValue + 2) % 4));
    }


    public Coords GetCoordsAfterRotation(Coords coords, Coords origin) {
        var dx = coords.x - origin.x;
        var dz = coords.z - origin.z;
        switch (IntValue) {
            default: return new Coords() { x = coords.x, z = coords.z };
            case 1: return new Coords() { x = origin.z - dz, z = coords.x };
            case 2: return new Coords() { x = origin.x - dx, z = origin.z - dz };
            case 3: return new Coords() { x = coords.z, z = origin.x - dx };
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

    public Coords GetCoordsAfterRotationBlock(Coords coords) {
        var mid = (Constants.BLOCK_SIZE - 1) / 2;
        return GetCoordsAfterRotation(coords, new Coords(mid, mid));
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
