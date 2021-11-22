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
    [SerializeField] GameObject underground;

    public void Init(Tile model, GridView gridView) {
        this.model = model;
        this.gridView = gridView;
        model.OnTileMode += SetMode;
        structurePlacer = StructurePlacementManager.Instance;
        model.OnHighlightColour += SetHighlightColour;
    }

    public void SetHighlightColour(Colour color) {
        highlight.SetActive(color != Colour.clear);
        var mesh = highlight.GetComponent<MeshRenderer>();
        switch (color) {
            case Colour.green: mesh.material = Materials.Instance.greenHighlight; break;
            case Colour.red: mesh.material = Materials.Instance.redHighlight; break;
            case Colour.white: mesh.material = Materials.Instance.whiteHighlight; break;
        }
    }

    public void MouseEnter() {
        structurePlacer.FocusTile(model);
    }

    public void MouseExit() {
        structurePlacer.FocusTile(null);
    }

    public void SetMode(TileMode mode) {
        path.SetActive(mode == TileMode.path);
        regular.SetActive(mode == TileMode.available);
        underground.SetActive(mode == TileMode.noBlock);
    }

    public void EnableHitbox(bool isActive) {
        GetComponent<BoxCollider>().enabled = isActive;
    }
}
