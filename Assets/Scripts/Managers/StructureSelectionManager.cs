using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSelectionManager : Singleton<StructureSelectionManager>
{
    [SerializeField] List<ButtonStructureView> buttons;

    private List<StructureData> availableStructures = new List<StructureData>();

    public void Init() {
        availableStructures.Add(StructureDataSet.GetEntry(0));
        availableStructures.Add(StructureDataSet.GetEntry(1));
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
