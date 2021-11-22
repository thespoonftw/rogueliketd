using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectHandler : Singleton<GameObjectHandler>
{   
    public static GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation) {
        return Instantiate(prefab, position, rotation, Instance.transform);
    }

    public static void Destroy(GameObject gameObject) {
        Destroy(gameObject);
    }

}
