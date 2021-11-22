using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : Singleton<Prefabs>
{
    public GameObject blockViewPrefab;
    public GameObject tileViewPrefab;
    public GameObject structurePrefab;

    public List<GameObject> structureModels;
}
