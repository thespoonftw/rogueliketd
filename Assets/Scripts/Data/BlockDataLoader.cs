using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BlockDataLoader {

    private List<BlockData> dict = new List<BlockData>();

    public BlockDataLoader() {
        var data = CsvLoader.LoadFile("Blocks");

        for (int i=0; i<data.Count; i += 8) {
            var blockData = data.Skip(i).Take(8).ToList();
            dict.Add(new BlockData(blockData));
        }

    }

    public BlockData GetData(int index) {
        return dict[index];
    }


}