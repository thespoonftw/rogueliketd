using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSelectionManager : Singleton<StructureSelectionManager>
{
    [SerializeField] List<ButtonStructureView> buttons;
    [SerializeField] GameObject downArrow;
    [SerializeField] GameObject upArrow;

    private List<StructureData> availableStructures = new List<StructureData>();
    private int currentPage = 0;

    public void Init() {
        availableStructures.Add(StructureDataSet.GetEntry(0)); // beacon
        availableStructures.Add(StructureDataSet.GetEntry(1)); // shrine
        availableStructures.Add(StructureDataSet.GetEntry(7)); // arrow tower
        availableStructures.Add(StructureDataSet.GetEntry(10)); // spike wall
        availableStructures.Add(StructureDataSet.GetEntry(13)); // floor spike
        availableStructures.Add(StructureDataSet.GetEntry(16)); // boulder
        availableStructures.Add(StructureDataSet.GetEntry(19)); // ballista
        availableStructures.Add(StructureDataSet.GetEntry(22)); // dart
        availableStructures.Add(StructureDataSet.GetEntry(25)); // bomb
        availableStructures.Add(StructureDataSet.GetEntry(30)); // swinging blade
        availableStructures.Add(StructureDataSet.GetEntry(31)); // spinning blade

    }

    public void StartSelection() {
        currentPage = 0;        
        UpdatePage();
        
    }

    private void UpdatePage() {
        upArrow.SetActive(currentPage > 0);
        downArrow.SetActive(availableStructures.Count > (currentPage + 1) * 6);
        for (int i = 0; i < 6; i++) {
            var structureIndex = i + (currentPage * 6);
            if (structureIndex >= availableStructures.Count) {
                buttons[i].Init(null);
            } else {
                buttons[i].Init(availableStructures[structureIndex]);
            }            
        }
    }

    public void DownArrowPressed() {
        currentPage++;
        UpdatePage();
    }

    public void UpArrowPressed() {
        currentPage--;
        UpdatePage();
    }



}
