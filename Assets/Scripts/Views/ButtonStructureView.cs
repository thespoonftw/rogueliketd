using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStructureView : MonoBehaviour {
    
    [SerializeField] Text text;
    [SerializeField] GameObject toggle;

    private StructureData structure;

    public void Init(StructureData structure) {
        toggle.SetActive(structure != null);
        this.structure = structure;
        if (structure != null) {
            text.text = structure.name + "\n[" + structure.cost + "]";
        }
        
    }

    public void Click() {
        StructurePlacementManager.Instance.StartPlacingStructure(structure);
        CanvasManager.Instance.SetState(CanvasState.structurePlacing);
    }

}
