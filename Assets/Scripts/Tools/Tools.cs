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
}


public class Tools : MonoBehaviour
{    public static Direction GetOppositeSide(Direction side) {
        return (Direction)((((int)side) + 2) % 4);
    }
}
