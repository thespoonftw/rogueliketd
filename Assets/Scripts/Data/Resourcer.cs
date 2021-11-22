using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(Resourcer))]
public class ResourcerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Resourcer myScript = (Resourcer)target;
        if (GUILayout.Button("Refresh Assets")) {
            myScript.UpdateAssets();
        }
    }

}

public class Resourcer : MonoBehaviour
{
    public void UpdateAssets() {
        var imageMaps = GetComponent<ImageMaps>();
        imageMaps.blockImageMaps = LoadImageMaps("BlockImageMaps");
        imageMaps.structurePlacementMaps = LoadImageMaps("StructurePlacementMaps");
        imageMaps.structureAreaMaps = LoadImageMaps("StructureAreaMaps");

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    public List<Texture2D> LoadImageMaps(string folderName) {
        var folder = Application.dataPath + "/Resources/" + folderName + "/";
        var files = Directory.GetFiles(folder);
        var maxInt = 0;
        foreach (var f in files) {
            maxInt = Mathf.Max(GetIntFromFilepath(f), maxInt);
        }
        var returner = new List<Texture2D>(new Texture2D[maxInt + 1]);
        foreach (var f in files) {
            if (f.Substring(f.Length - 4) == "meta") { continue; }
            var i = GetIntFromFilepath(f);
            var filename = f.Split('/').ToList().Last();
            var texture = Resources.Load<Texture2D>(folderName + "/" + filename.Split('.')[0]);
            var importer = (TextureImporter)TextureImporter.GetAtPath("Assets/Resources/" + folderName + "/" + filename);
            importer.isReadable = true;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.SaveAndReimport();
            returner[i] = texture;
        }
        return returner;
    }

    private int GetIntFromFilepath(string filePath) {
        var slashSplit = filePath.Split('/').ToList();
        var fileName = slashSplit.Last().Split('.')[0];
        var spaceSplit = fileName.Split(' ');
        return int.Parse(spaceSplit[0]);
    }
}
