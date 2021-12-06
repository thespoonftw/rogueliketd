using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataEnemyType : CsvDataEntry {

    public readonly string name;
    public readonly float normal;
    public readonly float heat;
    public readonly float atrophic;

    public DataEnemyType(List<string> line) {
        name = line[1];
        normal = float.Parse(line[2]);
        heat = float.Parse(line[3]);
        atrophic = float.Parse(line[4]);
    }
}