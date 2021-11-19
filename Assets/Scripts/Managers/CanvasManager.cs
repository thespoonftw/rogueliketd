using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CanvasState {
    standard,
    blockChoosing,
    blockPlacing,
    structurePlacing,
    structureChoosing,
}

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] GameObject standardToggle;
    [SerializeField] GameObject blockChooserToggle;
    [SerializeField] GameObject blockPlacingToggle;
    [SerializeField] GameObject structurePlacingToggle;
    [SerializeField] GameObject structureChoosingToggle;
    [SerializeField] Text goldText;

    public void SetState(CanvasState state) {
        standardToggle.SetActive(state == CanvasState.standard);
        blockChooserToggle.SetActive(state == CanvasState.blockChoosing);
        blockPlacingToggle.SetActive(state == CanvasState.blockPlacing);
        structurePlacingToggle.SetActive(state == CanvasState.structurePlacing);
        structureChoosingToggle.SetActive(state == CanvasState.structureChoosing);
    }

    public void StartChoosingBlock() {
        SetState(CanvasState.blockChoosing);
    }

    public void SendTraveller() {
        GameManager.Instance.SendTraveller();
    }

    public void BlockChosen(int index) {
        BlockPlacementManager.Instance.StartPlacingBlock(index);
        SetState(CanvasState.blockPlacing);
    }

    public void BlockCancelled() {
        BlockPlacementManager.Instance.StopBlockPlacement();
        SetState(CanvasState.blockChoosing);
    }

    public void FinishPlacingBlock() {
        SetState(CanvasState.standard);
    }

    public void FinishStructurePlacing() {
        StructurePlacementManager.Instance.StopPlacingStructure();
        SetState(CanvasState.structureChoosing);
    }

    public void StartStructureChoosing() {
        StructureSelectionManager.Instance.StartSelection();
        SetState(CanvasState.structureChoosing);
    }

    public void FinishStructureChoosing() {
        SetState(CanvasState.standard);
    }

    public void SetGoldText(int amount) {
        goldText.text = "Gold: " + amount.ToString();
    }

}
