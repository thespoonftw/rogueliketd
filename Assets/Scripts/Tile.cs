using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsTopPath => startPathArray[(0 + rotationIndex) % 4] != null;
    public bool IsRightPath => startPathArray[(1 + rotationIndex) % 4] != null;
    public bool IsBottomPath => startPathArray[(2 + rotationIndex) % 4] != null;
    public bool IsLeftPath => startPathArray[(3 + rotationIndex) % 4] != null;

    public Path topPath;
    public Path rightPath;
    public Path bottomPath;
    public Path leftPath;

    private Path[] startPathArray;
    private int rotationIndex = 0;

    public void Init(int rotationIndex)
    {
        startPathArray = new Path[4] { topPath, rightPath, bottomPath, leftPath };
        this.rotationIndex = rotationIndex % 4;
        transform.rotation = Quaternion.Euler(0, 0, this.rotationIndex * 90);
    }
}
