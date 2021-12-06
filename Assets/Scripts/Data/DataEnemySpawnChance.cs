using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEnemySpawnChance : CsvDataEntry {

    private readonly float tinyChance;
    private readonly float smallChance;
    private readonly float mediumChance;
    private readonly float largeChance;
    private readonly float giantChance;

    public DataEnemySpawnChance(List<string> entries) {
        tinyChance = float.Parse(entries[1]);
        smallChance = float.Parse(entries[2]);
        mediumChance = float.Parse(entries[3]);
        largeChance = float.Parse(entries[4]);
        giantChance = float.Parse(entries[5]);
    }

    public int RandomlySelectSize() {
        var randomInt = UnityEngine.Random.Range(0, 100);
        var chanceArray = new List<float>();
        chanceArray.Add(tinyChance);
        chanceArray.Add(tinyChance + smallChance);
        chanceArray.Add(tinyChance + smallChance + mediumChance);
        chanceArray.Add(tinyChance + smallChance + mediumChance + largeChance);
        chanceArray.Add(tinyChance + smallChance + mediumChance + largeChance + giantChance);
        int index = -1;
        for (int i=0; i<5; i++) {
            if (randomInt < chanceArray[i]) {
                index = i;
                break;
            }
        }
        return index;
    }

}
