using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Grid GameGrid { get; private set; }
    public GridView GameGridView { get; private set; }
    public int Gold { get; private set; }
    public int Lives { get; private set; }

    public void Start() {
        GameGrid = new Grid(Constants.GAME_GRID_SIZE);
        var go = new GameObject("Game Grid");
        GameGridView = go.AddComponent<GridView>();
        GameGridView.Init(GameGrid);
        Data.Init();

        BlockPlacementManager.Instance.Init();
        StructurePlacementManager.Instance.Init();
        StructureSelectionManager.Instance.Init();

        var halfWidth = (Constants.GAME_GRID_SIZE - 1) / 2;
        BlockPlacementManager.Instance.PlaceBlockWithCode(new Coords(halfWidth, halfWidth), 0, new Direction());

        CanvasManager.Instance.Init();

        

        ModifyGold(500);
        ModifyLives(25);
    }

    public void ModifyGold(int amount) {
        Gold += amount;
        CanvasManager.Instance.SetGoldText(Gold);
    }

    public void ModifyLives(int amount) {
        Lives += amount;
        CanvasManager.Instance.SetLivesText(Lives);
    }
}
