using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Grid GameGrid { get; private set; }
    public GridView GameGridView { get; private set; }
    public int Gold { get; private set; }
    public StructureData StructureData { get; private set; }

    [SerializeField] GameObject travellerPrefab;

    public void Start() {
        GameGrid = new Grid(Constants.GAME_GRID_SIZE);
        var go = new GameObject("Game Grid");
        GameGridView = go.AddComponent<GridView>();
        GameGridView.Init(GameGrid);
        StructureData = new StructureData();

        BlockPlacementManager.Instance.Init();
        StructurePlacementManager.Instance.Init();
        StructureSelectionManager.Instance.Init();

        var halfWidth = (Constants.GAME_GRID_SIZE - 1) / 2;
        BlockPlacementManager.Instance.PlaceBlockWithCode(halfWidth, halfWidth, 0);

        CanvasManager.Instance.SetState(CanvasState.standard);

        

        ModifyGold(100);
    }

    public void SendTraveller() {
        var go = Instantiate(travellerPrefab, new Vector3(), Quaternion.identity, transform);
        go.GetComponent<TravellerView>().Init();
    }

    public void FinishTravellerPath() {
        // do something?
    }

    public void ModifyGold(int amount) {
        Gold += amount;
        CanvasManager.Instance.SetGoldText(Gold);
    }
}
