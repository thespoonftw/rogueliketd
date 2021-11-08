using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasState {
    standard,
    blockChoosing,
    blockPlacing,
    structurePlacing,
}

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] GameObject standard;
    [SerializeField] GameObject blockChooser;
    [SerializeField] GameObject blockPlacing;
    [SerializeField] GameObject structurePlacing;

    public void SetState(CanvasState state) {
        standard.SetActive(state == CanvasState.standard);
        blockChooser.SetActive(state == CanvasState.blockChoosing);
        blockPlacing.SetActive(state == CanvasState.blockPlacing);
        structurePlacing.SetActive(state == CanvasState.structurePlacing);
    }

    public void StartPlacingStructure() {
        StructurePlacementManager.Instance.StartPlacingStructure();
        SetState(CanvasState.structurePlacing);
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
        BlockPlacementManager.Instance.CancelPlacement();
        SetState(CanvasState.blockChoosing);
    }

    public void FinishPlacingBlock() {
        SetState(CanvasState.standard);
    }

    public void FinishStructurePlacing() {
        StructurePlacementManager.Instance.StopPlacingStructure();
        SetState(CanvasState.standard);
    }

}
