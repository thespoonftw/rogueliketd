using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Start() {
        var n = (Constants.numberOfTiles - 1) / 2;
        PlacementTileManager.Instance.ForcePlaceTile(n, n, 0);
    }
}
