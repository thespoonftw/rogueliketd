using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSelectionManager : Singleton<StructureSelectionManager>
{
    [SerializeField] List<ButtonStructureView> buttons;
    [SerializeField] GameObject downArrow;
    [SerializeField] GameObject upArrow;

    private List<DataStructure> availableStructures = new List<DataStructure>();
    private int currentPage = 0;

    public void Init() {
        availableStructures.Add(Data.Structures.GetEntry(0)); // beacon
        availableStructures.Add(Data.Structures.GetEntry(1)); // shrine
        availableStructures.Add(Data.Structures.GetEntry(7)); // arrow tower
        availableStructures.Add(Data.Structures.GetEntry(10)); // spike wall
        availableStructures.Add(Data.Structures.GetEntry(13)); // floor spike
        availableStructures.Add(Data.Structures.GetEntry(16)); // boulder
        availableStructures.Add(Data.Structures.GetEntry(19)); // ballista
        availableStructures.Add(Data.Structures.GetEntry(22)); // dart
        availableStructures.Add(Data.Structures.GetEntry(25)); // bomb
        availableStructures.Add(Data.Structures.GetEntry(30)); // swinging blade
        availableStructures.Add(Data.Structures.GetEntry(31)); // spinning blade

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
