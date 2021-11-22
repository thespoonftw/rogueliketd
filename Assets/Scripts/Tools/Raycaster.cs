using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RaycastMode {
    tiles,
    blocks,
    standard,
}

public class Raycaster : MonoBehaviour {

    private RaycastMode mode;
    private static Raycaster instance;
    private GameObject currentTarget;

    private void Start() {
        instance = this;
        SetMode(RaycastMode.standard);
    }

    private void Update() {

        if (EventSystem.current.IsPointerOverGameObject(-1)) {
            UpdateTarget(null);
        } else {
            var isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000f, GetLayerMask());
            UpdateTarget(isHit ? hit.collider.gameObject : null);
        }
    }        

    private void UpdateTarget(GameObject newTarget) {
        if (currentTarget == newTarget) { return; }
        var oldTarget = currentTarget;
        currentTarget = newTarget;

        switch (mode) {
            case RaycastMode.tiles:
                if (oldTarget != null) { oldTarget.GetComponent<TileView>().MouseExit(); }
                if (currentTarget != null) { currentTarget?.GetComponent<TileView>().MouseEnter(); }
                break;
            case RaycastMode.blocks:
                if (oldTarget != null) { oldTarget.GetComponent<BlockView>().MouseExit(); }
                if (currentTarget != null) { currentTarget?.GetComponent<BlockView>().MouseEnter(); }
                break;
        }
    }

    private int GetLayerMask() {
        switch (mode) {
            case RaycastMode.tiles: return LayerMask.GetMask("Tile");
            case RaycastMode.blocks: return LayerMask.GetMask("Block");
            case RaycastMode.standard: return LayerMask.GetMask("Entity");
        }
        return 0;
    }

    public static void SetMode(RaycastMode mode) {
        instance.currentTarget = null;
        instance.mode = mode;
    }
}
