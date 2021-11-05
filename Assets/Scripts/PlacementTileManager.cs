using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTileManager : Singleton<PlacementTileManager>
{
    [SerializeField] GameObject placementTilePrefab;
    [SerializeField] GameObject startTile;
    [SerializeField] GameObject leftTile;
    [SerializeField] GameObject rightTile;
    [SerializeField] GameObject straightTile;

    private PlacementTile[,] placementTileArray;
    private Tile[,] tileArray;
    private GameObject tileToBePlaced;


    private GameObject highlightPlacementTile;
    private bool isPlacingTileEnabled = false;
    private int highlightedX;
    private int highlightedY;
    private bool isValidLocation = false;
    private int rotationIndex;

    private void Start() {

        var n = Constants.numberOfTiles;
        placementTileArray = new PlacementTile[n, n];
        tileArray = new Tile[n, n];

        var halfWidth = (n - 1) / 2;

        for (int x = 0; x < n; x++) {
            for (int y = 0; y < n; y++) {
                var go = Instantiate(placementTilePrefab, new Vector3((x - halfWidth) * Constants.tileWidth, (y - halfWidth) * Constants.tileWidth), Quaternion.identity, transform);
                go.name = "Tile[" + x + "," + y + "]";
                var view = go.GetComponent<PlacementTile>();
                view.Initialise(x, y);
                placementTileArray[x, y] = view;
            }
        }

        ForcePlaceTile(halfWidth, halfWidth, startTile);
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1)) {
            RotateTile();
        }
        if (Input.GetMouseButtonUp(0)) {
            UserPlaceTile(highlightedX, highlightedY);
        }
    }

    public void TryHighlightPlacementTile(int x, int y) {
        if (!isPlacingTileEnabled) { return; }
        highlightPlacementTile = Instantiate(tileToBePlaced, placementTileArray[x, y].transform.position + new Vector3(0, 0, 1), Quaternion.identity, transform);
        var tile = highlightPlacementTile.GetComponent<Tile>();
        tile.Init(rotationIndex);
        if (IsValidTilePlacement(x,y))
        {
            placementTileArray[x, y].SetColour(PlacementTileColor.green);
            isValidLocation = true;
        }
        else
        {
            placementTileArray[x, y].SetColour(PlacementTileColor.red);
            isValidLocation = false;
        }
        
        highlightedX = x;
        highlightedY = y;
        
    }

    private bool IsValidTilePlacement(int x, int y) {
        if (tileArray[x,y] != null) { return false; }
        if (tileArray[x + 1, y] == null && tileArray[x - 1, y] == null && tileArray[x, y + 1] == null && tileArray[x, y - 1] == null) { return false; }

        var tile = highlightPlacementTile.GetComponent<Tile>();

        if (tileArray[x + 1, y] != null) {
            if (tileArray[x + 1, y].GetComponent<Tile>().IsLeftPath != tile.IsRightPath) { return false; }
        }
        if (tileArray[x - 1, y] != null) {
            if (tileArray[x - 1, y].GetComponent<Tile>().IsRightPath != tile.IsLeftPath) { return false; }
        }
        if (tileArray[x, y + 1] != null) {
            if (tileArray[x, y + 1].GetComponent<Tile>().IsBottomPath != tile.IsTopPath) { return false; }
        }
        if (tileArray[x, y - 1] != null) {
            if (tileArray[x, y - 1].GetComponent<Tile>().IsTopPath != tile.IsBottomPath) { return false; }
        }

        return true;
    }
    
    public void UnHighlightPlacementTile(int x, int y) {
        Destroy(highlightPlacementTile);
        placementTileArray[x, y].SetColour(PlacementTileColor.clear);
    }

    public void StartPlacingTile(int tileIndex) {
        switch (tileIndex) {
            case 0: tileToBePlaced = startTile; break;
            case 1: tileToBePlaced = leftTile; break;
            case 2: tileToBePlaced = straightTile; break;
            case 3: tileToBePlaced = rightTile; break;
        }
        isPlacingTileEnabled = true;
        rotationIndex = 0;
    }

    public void UserPlaceTile(int x, int y) {
        if (!isPlacingTileEnabled || !isValidLocation) { return; }
        ForcePlaceTile(x, y, tileToBePlaced, rotationIndex);
        isPlacingTileEnabled = false;
        CanvasManager.Instance.ShowTileChooser();
        isValidLocation = false;
    }

    public void RotateTile() {
        rotationIndex = (rotationIndex + 1) % 4;
        Destroy(highlightPlacementTile);
        TryHighlightPlacementTile(highlightedX, highlightedY);
    }

    public void ForcePlaceTile(int x, int y, GameObject tilePrefab, int rotation = 0) {        
        var currentTile = placementTileArray[x, y];
        var go = Instantiate(tilePrefab, currentTile.transform.position, Quaternion.Euler(0, 0, rotation * 90), transform);
        var tile = go.GetComponent<Tile>();
        tile.Init(rotationIndex);
        tileArray[x, y] = tile;
    }
}
