using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CsvData<T> where T : CsvDataEntry {

    private static List<T> list = new List<T>();

    public CsvData(string filename) {
        var data = LoadFile(filename);
        foreach (var d in data) {
            var entry = (T)Activator.CreateInstance(typeof(T), new object[] { d });
            list.Add(entry);
        }
    }

    private List<List<string>> LoadFile(string filename) {
        var returner = new List<List<string>>();
        var filepath = Application.streamingAssetsPath + "/Data/" + filename + ".csv";
        try {
            var filedata = System.IO.File.ReadAllText(filepath);
            var chars = new Char[2] { "\r"[0], "\n"[0] };
            var lines = filedata.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++) {
                returner.Add(lines[i].Split(',').ToList());
            }
            return returner;
        } catch {
            Debug.Log("Unable to read " + filename + ". Is it open or missing?");
            return null;
        }
    }

    public T GetEntry(int index) {
        return list[index];
    }
}

public abstract class CsvDataEntry {

    public CsvDataEntry() {

    }

    public CsvDataEntry(List<string> line) {

    }
}