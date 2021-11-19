using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour {

    private GridView gridView;
    private Tile model;
    private StructurePlacementManager structurePlacer;

    [SerializeField] GameObject highlight;
    [SerializeField] GameObject path;
    [SerializeField] GameObject regular;

    public void Init(Tile model, GridView gridView) {
        this.model = model;
        this.gridView = gridView;
        model.OnPath += ShowPath;
        model.OnClear += ClearPath;
        structurePlacer = StructurePlacementManager.Instance;
        model.OnHighlightColour += SetHighlightColour;
    }

    public void SetHighlightColour(Colour color) {
        highlight.color = color == Colour.red ? new Color(1, 0, 0, 0.5f) : new Color(0, 1, 0, 0.5f);
        highlight.enabled = color != Colour.clear;
    }

    private void OnMouseEnter() {
        structurePlacer.TryHighlightTile(model);
    }

    private void OnMouseExit() {
        structurePlacer.RemoveHighlight(model);
    }

    public void ShowPath() {
        path.enabled = true;
    }

    public void ClearPath() {
        path.enabled = false;
    }

    public void EnableHitbox(bool isActive) {
        GetComponent<BoxCollider2D>().enabled = isActive;
    }
}
