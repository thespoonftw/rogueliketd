using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMaps : MonoBehaviour
{
    public List<Texture2D> blockImageMaps;
    public List<Texture2D> structurePlacementMaps;
    public List<Texture2D> structureAreaMaps;
   
    private static ImageMaps instance;

    void Start() {
        instance = this;
    }

    public static Texture2D GetBlockMap(int index) {
        return instance.blockImageMaps[index];
    }

    public static Texture2D GetStructurePlacementMap(int index) {
        return instance.structurePlacementMaps[index];
    }

    public static Texture2D GetStructureAreaMap(int index) {
        return instance.structureAreaMaps[index];
    }
}
