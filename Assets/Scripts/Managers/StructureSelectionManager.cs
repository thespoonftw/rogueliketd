using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSelectionManager : Singleton<StructureSelectionManager>
{
    [SerializeField] List<ButtonStructureView> buttons;

    private List<StructureDataEntry> availableStructures = new List<StructureDataEntry>();

    public void Init() {
        var data = GameManager.Instance.StructureData;
        availableStructures.Add(data.GetEntry(0));
        availableStructures.Add(data.GetEntry(1));
    }

    public void StartSelection() {
        buttons[0].Init(availableStructures[0]);
        buttons[1].Init(availableStructures[1]);
        buttons[2].Init(null);
        buttons[3].Init(null);
        buttons[4].Init(null);
        buttons[5].Init(null);
    }



}
