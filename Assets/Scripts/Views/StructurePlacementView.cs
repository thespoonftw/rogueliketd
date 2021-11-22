using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructurePlacementView : MonoBehaviour {

    private GameManager game;
    private GameObject ghostStructure;
    private List<Tile> highlightedTiles;

    private void Start() {
        game = GameManager.Instance;
        StructurePlacementManager.Instance.OnFocusTile += FocusTile;
    }

    private void FocusTile(Tile tile, bool isValid, StructureData data, int rotationIndex) {
        if (ghostStructure != null) { Destroy(ghostStructure); }       
        if (highlightedTiles != null) { 
            highlightedTiles.ForEach(t => t.SetHighlight(Colour.clear));
            highlightedTiles = null;
        }

        if (tile != null) {
            var pos = game.GameGridView.GetTilePosition(tile);
            ghostStructure = Instantiate(Prefabs.Instance.structureModels[data.modelIndex], pos, Tools.GetRotation(rotationIndex), transform);
            var meshes = ghostStructure.GetComponentsInChildren<MeshRenderer>().ToList();
            meshes.ForEach(m => m.material = isValid ? Materials.Instance.greenHighlight : Materials.Instance.redHighlight);
            highlightedTiles = new List<Tile>();

            var half = (Constants.MAX_STRUCTURE_AREA - 1) / 2;
            for (int x = 0; x < Constants.MAX_STRUCTURE_AREA; x++) {
                for (int z = 0; z < Constants.MAX_STRUCTURE_AREA; z++) {
                    var coords = Tools.GetCoordsAfterRotation(rotationIndex, x, z, half, half);
                    if (!data.GetIsArea(coords.x, coords.z)) { continue; }
                    var t = game.GameGrid.GetTile(x + tile.X - half, z + tile.Z - half);
                    highlightedTiles.Add(t);
                    t.SetHighlight(Colour.white);
                }
            }
        } else {

        }
        
    }
}
