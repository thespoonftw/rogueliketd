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
    [SerializeField] GameObject roundNotInProgressToggle;
    [SerializeField] GameObject blockChooserToggle;
    [SerializeField] GameObject blockPlacingToggle;
    [SerializeField] GameObject structurePlacingToggle;
    [SerializeField] GameObject structureChoosingToggle;
    [SerializeField] Text goldText;
    [SerializeField] Text livesText;

    private bool isRoundInProgress;
    private CanvasState currentState;

    public void Init() {
        SetState(CanvasState.standard);
        WaveManager.Instance.OnRoundInProgress += SetRoundInProgress;
    }

    public void SetState(CanvasState state) {
        currentState = state;
        UpdateState();
    }

    public void SetRoundInProgress(bool isInProgress) {
        isRoundInProgress = isInProgress;
        UpdateState();
    }

    private void UpdateState() {
        standardToggle.SetActive(currentState == CanvasState.standard);
        roundNotInProgressToggle.SetActive(currentState == CanvasState.standard && !isRoundInProgress);
        blockChooserToggle.SetActive(currentState == CanvasState.blockChoosing);
        blockPlacingToggle.SetActive(currentState == CanvasState.blockPlacing);
        structurePlacingToggle.SetActive(currentState == CanvasState.structurePlacing);
        structureChoosingToggle.SetActive(currentState == CanvasState.structureChoosing);
    }

    public void StartChoosingBlock() {
        SetState(CanvasState.blockChoosing);
    }

    public void StartRound() {
        WaveManager.Instance.StartWave();
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

    public void SetLivesText(int amount) {
        livesText.text = "Lives: " + amount.ToString();
    }

}
