using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsTopPath => topPath != null;
    public bool IsBottomPath => bottomPath != null;
    public bool IsRightPath => rightPath != null;
    public bool IsLeftPath => leftPath != null;

    public Path topPath;
    public Path rightPath;
    public Path bottomPath;
    public Path leftPath;
}
