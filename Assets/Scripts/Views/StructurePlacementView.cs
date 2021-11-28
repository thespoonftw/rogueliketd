using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructurePlacementView : MonoBehaviour {

    private GameManager game;
    private GameObject ghostStructure;
    private List<Tile> highlightedTiles;

    [SerializeField] TowerRangeView towerRangeView;

    private void Start() {
        game = GameManager.Instance;
        StructurePlacementManager.Instance.OnFocusTile += FocusTile;
    }

    private void FocusTile(Tile tile, bool isValid, StructureData data, int rotationIndex) {
        if (tile == null) {
            Destroy(ghostStructure);
            towerRangeView.Hide();
        }   
        if (highlightedTiles != null) { 
            highlightedTiles.ForEach(t => t.SetHighlight(Colour.clear));
            highlightedTiles = null;
        }

        if (tile != null) {
            var pos = game.GameGridView.GetTilePosition(tile);
            if (ghostStructure == null) {
                ghostStructure = Instantiate(Prefabs.Instance.structureModels[data.modelIndex], pos, Tools.GetRotation(rotationIndex), transform);
            } else {
                ghostStructure.transform.position = pos;
                ghostStructure.transform.rotation = Tools.GetRotation(rotationIndex);
            }
            var meshes = ghostStructure.GetComponentsInChildren<MeshRenderer>().ToList();
            meshes.ForEach(m => m.material = isValid ? Materials.Instance.greenHighlight : Materials.Instance.redHighlight);
            highlightedTiles = new List<Tile>();
            towerRangeView.ShowRange(data, tile, rotationIndex);

            /* this is causing lag :( but its not needed
            var half = (Constants.BLOCK_SIZE - 1) / 2;
            for (int x = 0; x < Constants.BLOCK_SIZE; x++) {
                for (int z = 0; z < Constants.BLOCK_SIZE; z++) {
                    var coords = Tools.GetCoordsAfterRotation(rotationIndex, x, z, half, half);
                    if (!data.GetIsArea(coords.x, coords.z)) { continue; }
                    var t = game.GameGrid.GetTile(x + tile.X - half, z + tile.Z - half);
                    highlightedTiles.Add(t);
                    t.SetHighlight(Colour.white);
                }
            }
            */
        } else {

        }
        
    }
}
