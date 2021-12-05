using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEnemySpawnChance : CsvDataEntry {

    public readonly float tinyChance;
    public readonly float smallChance;
    public readonly float mediumChance;
    public readonly float largeChance;
    public readonly float giantChance;

    public DataEnemySpawnChance(List<string> entries) {
        tinyChance = float.Parse(entries[0]);
        smallChance = float.Parse(entries[1]);
        mediumChance = float.Parse(entries[2]);
        largeChance = float.Parse(entries[3]);
        giantChance = float.Parse(entries[4]);
    }

}
