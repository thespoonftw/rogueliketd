using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureView : MonoBehaviour {

    public void Init(StructureData data, int rotationIndex) {
        var model = Instantiate(Prefabs.Instance.structureModels[data.modelIndex], transform.position, Tools.GetRotation(rotationIndex), transform);
    }
}
