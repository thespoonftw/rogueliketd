using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockView : MonoBehaviour {

    private GridView gridView;
    private Block model;
    private BlockPlacementManager blockPlacer;

    [SerializeField] GameObject highlight;

    public void Init(Block model, GridView gridView) {
        this.model = model;
        this.gridView = gridView;
        blockPlacer = BlockPlacementManager.Instance;
        model.OnHighlightColour += SetHighlightColour;
    }

    public void SetHighlightColour(Colour color) {
        highlight.GetComponent<SpriteRenderer>().color = color == Colour.red ? new Color(1, 0, 0, 0.5f) : new Color(0, 1, 0, 0.5f);
        highlight.SetActive(color != Colour.clear);
    }

    public void MouseEnter() {
        blockPlacer.TryHighlightBlock(model);
    }

    public void MouseExit() {
        blockPlacer.RemoveHighlight(model);
    }

    public void EnableHitbox(bool isActive) {
        GetComponent<BoxCollider>().enabled = isActive;
    }

}
