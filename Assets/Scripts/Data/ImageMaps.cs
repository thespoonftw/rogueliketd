using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMaps : MonoBehaviour
{
    public List<Texture2D> blockImageMaps;
    public List<Texture2D> structureMaps;
   
    private static ImageMaps instance;

    void Start() {
        instance = this;
    }

    public static Texture2D GetBlockMap(int index) {
        return instance.blockImageMaps[index];
    }

    public static Texture2D GetStructureMap(int index) {
        return instance.structureMaps[index];
    }
}
