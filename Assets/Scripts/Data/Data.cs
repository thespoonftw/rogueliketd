using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Data {

    public static CsvData<DataStructure> Structures;
    public static CsvData<DataBlock> Blocks;
    public static CsvData<DataEnemySpawnChance> EnemySpawnChance;

    public static void Init() {
        Structures = new CsvData<DataStructure>("Structures", x => new DataStructure(x));
        Blocks = new CsvData<DataBlock>("Blocks", x => new DataBlock(x));
        EnemySpawnChance = new CsvData<DataEnemySpawnChance>("EnemySpawnChance", x => new DataEnemySpawnChance(x));

    }
}