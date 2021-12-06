using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataEnemyStat : CsvDataEntry {

    public readonly string name;
    public readonly int points;
    public readonly float speed;
    public readonly int health;
    public readonly float scale;

    public DataEnemyStat(List<string> line) {
        name = line[1];
        points = int.Parse(line[2]);
        speed = float.Parse(line[3]);
        health = int.Parse(line[4]);
        scale = float.Parse(line[5]);
    }
}