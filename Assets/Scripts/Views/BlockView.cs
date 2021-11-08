using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockView : MonoBehaviour {

    private GridView gridView;
    private Block model;
    private BlockPlacementManager blockPlacer;

    [SerializeField] SpriteRenderer background;
    [SerializeField] SpriteRenderer highlight;

    public void Init(Block model, GridView gridView) {
        this.model = model;
        this.gridView = gridView;
        blockPlacer = BlockPlacementManager.Instance;
        model.OnHighlightColour += SetHighlightColour;
        model.OnIsPlaced += Place;
        model.OnClear += Clear;
    }

    public void SetHighlightColour(Colour color) {
        highlight.color = color == Colour.red ? new Color(1, 0, 0, 0.5f) : new Color(0, 1, 0, 0.5f);
        highlight.enabled = color != Colour.clear;
    }

    public void Place() {
        background.enabled = true;
    }

    public void Clear() {
        background.enabled = false;
    }

    private void OnMouseEnter() {
        blockPlacer.TryHighlightBlock(model);
    }

    private void OnMouseExit() {
        blockPlacer.RemoveHighlight(model);
    }

    private void Destroy() {
        model.OnClear -= Destroy;
        model.OnHighlightColour -= SetHighlightColour;
        Destroy(gameObject);
    }

}
