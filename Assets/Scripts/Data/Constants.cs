using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static int BLOCK_SIZE = 7;
    public static int MAX_STRUCTURE_AREA = 15;
    public static int GAME_GRID_SIZE = 15; // make sure this is odd
    public static List<Direction> ALL_DIRECTIONS = new List<Direction>() { Direction.north, Direction.east, Direction.south, Direction.west };
}