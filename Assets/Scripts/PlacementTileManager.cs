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

    private PlacementTileView[,] tileArray;
    private GameObject highlightPlacementTile;
    private int rotationIndex = 0;
    private bool isPlacingTileEnabled = false;
    private int tileTypeIndex = 0;
    private int highlightedX;
    private int highlightedY;
    private bool isValidLocation = false;

    private void Start() {

        var n = Constants.numberOfTiles;
        tileArray = new PlacementTileView[n, n];
        var halfWidth = (n - 1) / 2;

        for (int x = 0; x < n; x++) {
            for (int y = 0; y < n; y++) {
                var go = Instantiate(placementTilePrefab, new Vector3((x - halfWidth) * Constants.tileWidth, (y - halfWidth) * Constants.tileWidth), Quaternion.identity, transform);
                go.name = "Tile[" + x + "," + y + "]";
                var view = go.GetComponent<PlacementTileView>();
                view.Initialise(x, y);
                tileArray[x, y] = view;
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
        highlightPlacementTile = Instantiate(GetTileType(tileTypeIndex), tileArray[x, y].transform.position + new Vector3(0, 0, 1), rotation, transform);
        tileArray[x, y].SetColour(true);
        highlightedX = x;
        highlightedY = y;
        isValidLocation = true;
    }
    
    public void UnHighlightPlacementTile(int x, int y) {
        Destroy(highlightPlacementTile);
        tileArray[x, y].SetColour(false);
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
        var currentTile = tileArray[x, y];
        var newTile = Instantiate(GetTileType(tileIndex), currentTile.transform.position, Quaternion.Euler(0, 0, rotation * 90), transform);       
    }
}
