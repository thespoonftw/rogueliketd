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


    private GameObject highlightPlacementTile;
    private int rotationIndex = 0;
    private bool isPlacingTileEnabled = false;
    private int tileTypeIndex = 0;
    private int highlightedX;
    private int highlightedY;
    private bool isValidLocation = false;

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
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1)) {
            RotateTile();
        }
        if (Input.GetMouseButtonUp(0)) {
            UserPlaceTile(highlightedX, highlightedY);
        }
    }

    private GameObject GetTileType(int tileTypeIndex) {
        switch (tileTypeIndex) {
            case 0: return startTile;
            case 1: return leftTile;
            case 2: return straightTile;
            case 3: return rightTile;
            default: return null;        
        }
    }

    public void TryHighlightPlacementTile(int x, int y) {
        if (!isPlacingTileEnabled) { return; }
        var rotation = Quaternion.Euler(0, 0, rotationIndex * 90);
        highlightPlacementTile = Instantiate(GetTileType(tileTypeIndex), placementTileArray[x, y].transform.position + new Vector3(0, 0, 1), rotation, transform);
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

    private bool IsValidTilePlacement(int x, int y)
    {
        if (tileArray[x,y] != null) { return false; }
        if (tileArray[x + 1, y] == null && tileArray[x - 1, y] == null && tileArray[x, y + 1] == null && tileArray[x, y - 1] == null) { return false; }
        return true;
    }
    
    public void UnHighlightPlacementTile(int x, int y) {
        Destroy(highlightPlacementTile);
        placementTileArray[x, y].SetColour(PlacementTileColor.clear);
    }

    public void StartPlacingTile(int tileIndex) {
        tileTypeIndex = tileIndex;
        isPlacingTileEnabled = true;
        rotationIndex = 0;
    }

    public void UserPlaceTile(int x, int y) {
        if (!isPlacingTileEnabled || !isValidLocation) { return; }
        ForcePlaceTile(x, y, tileTypeIndex, rotationIndex);
        isPlacingTileEnabled = false;
        CanvasManager.Instance.ShowTileChooser();
        isValidLocation = false;
    }

    public void RotateTile() {
        rotationIndex++;
        if (rotationIndex == 4) { rotationIndex = 0; }
        Destroy(highlightPlacementTile);
        TryHighlightPlacementTile(highlightedX, highlightedY);
    }

    public void ForcePlaceTile(int x, int y, int tileIndex, int rotation = 0) {        
        var currentTile = placementTileArray[x, y];
        var go = Instantiate(GetTileType(tileIndex), currentTile.transform.position, Quaternion.Euler(0, 0, rotation * 90), transform);
        var tile = go.GetComponent<Tile>();
        tileArray[x, y] = tile;
    }
}
