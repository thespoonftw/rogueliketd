using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Grid gameGrid;

    [SerializeField] GameObject travellerPrefab;
    [SerializeField] GridView gridView;

    public void Start() {
        gameGrid = new Grid(Constants.GAME_GRID_SIZE);
        gridView.Init(gameGrid);
        BlockPlacementManager.Instance.Init();

        var halfWidth = (Constants.GAME_GRID_SIZE - 1) / 2;
        BlockPlacementManager.Instance.PlaceBlockWithCode(halfWidth, halfWidth, 0);

        CanvasManager.Instance.SetState(CanvasState.standard);
    }

    public void SendTraveller() {
        var go = Instantiate(travellerPrefab, new Vector3(), Quaternion.identity, transform);
        go.GetComponent<TravellerView>().Init();
    }

    public void FinishTravellerPath() {
        // do something?
    }
}
