using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureData {

    private List<StructureDataEntry> list = new List<StructureDataEntry>();

    public StructureData() {
        var data = CsvLoader.LoadFile("Structures");
        data.ForEach(d => list.Add(new StructureDataEntry(d)));

    }

    public StructureDataEntry GetEntry(int index) {
        return list[index];
    }
}

public class StructureDataEntry {

    public readonly string name;
    public readonly int cost;

    public StructureDataEntry(List<string> line) {
        name = line[0];
        cost = int.Parse(line[1]);
    }
    
}
