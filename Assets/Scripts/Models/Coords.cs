using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Coords {

    public int x;
    public int z;

    public static Coords Zero { get { return new Coords(0, 0); } }

    public Coords(int x, int z) {
        this.x = x;
        this.z = z;

    }

    public Coords SouthOne => new Coords(x, z - 1);
    public Coords NorthOne => new Coords(x, z + 1);
    public Coords EastOne => new Coords(x + 1, z);
    public Coords WestOne => new Coords(x - 1, z);

    public Coords Neighbour(DirectionValue dir) {
        switch (dir) {
            case DirectionValue.north: return NorthOne;
            case DirectionValue.east: return EastOne;
            case DirectionValue.south: return SouthOne;
            default: return WestOne;
        }
    }

    public override string ToString() {
        return x + "," + z;
    }

}
