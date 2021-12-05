using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureView : MonoBehaviour {

    public void Init(DataStructure data, Direction direction) {
        var model = Instantiate(Prefabs.Instance.structureModels[data.modelIndex], transform.position, direction.Quaternion, transform);
    }
}
