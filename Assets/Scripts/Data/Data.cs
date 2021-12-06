using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Data {

    public static CsvData<DataStructure> Structures;
    public static CsvData<DataBlock> Blocks;
    public static CsvData<DataEnemySpawnChance> EnemySpawnChances;
    public static CsvData<DataEnemyStat> EnemyStats;
    public static CsvData<DataEnemyType> EnemyTypes;

    public static void Init() {
        Structures = new CsvData<DataStructure>("Structures");
        Blocks = new CsvData<DataBlock>("Blocks");
        EnemySpawnChances = new CsvData<DataEnemySpawnChance>("EnemySpawnChance");
        EnemyStats = new CsvData<DataEnemyStat>("EnemyStats");
        EnemyTypes = new CsvData<DataEnemyType>("EnemyTypes");

    }
}