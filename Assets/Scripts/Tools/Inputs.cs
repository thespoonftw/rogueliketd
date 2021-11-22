using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {

    //public static event Action<bool> OnMouseScrollIsUp;
    //public static event Action OnLeftMousePress;
    //public static event Action OnLeftMouseRelease;
    //public static event Action OnRightMouseDown;
    public static event Action OnLeftMouseQuickRelease;
    public static event Action OnRightMouseQuickRelease;
    //public static event Action<Vector3> OnRightMouseDrag;

    private float leftMouseDownTime;
    private Vector3 leftMouseStartPosition;
    private float rightMouseDownTime;
    private Vector3 rightMouseStartPosition;

    private void Update() {
        MouseClicks();
    }

    /*
    private void MouseScroll() {
        if (Input.mouseScrollDelta.y < 0) {
            OnMouseScrollIsUp?.Invoke(true);

        } else if (Input.mouseScrollDelta.y > 0) {
            OnMouseScrollIsUp?.Invoke(false);
        }
    }
    */

    private void MouseClicks() {
        /*
        if (Input.GetMouseButtonUp(0)) {
            OnLeftMouseRelease?.Invoke();
        }

        if (Input.GetMouseButton(1)) {
            rightMouseDownTime += Time.fixedUnscaledDeltaTime;
        }
                */

        if (Input.GetMouseButtonDown(0)) {
            //OnLeftMousePress?.Invoke();
            leftMouseStartPosition = Input.mousePosition;
            leftMouseDownTime = 0;
        }

        if (Input.GetMouseButtonDown(1)) {
            //OnRightMouseDown?.Invoke();
            rightMouseStartPosition = Input.mousePosition;
            rightMouseDownTime = 0;
        }

        if (Input.GetMouseButtonUp(0) && leftMouseDownTime < 0.5f && Vector3.Distance(Input.mousePosition, leftMouseStartPosition) < 20f) {
            OnLeftMouseQuickRelease?.Invoke();
        }

        if (Input.GetMouseButtonUp(1) && rightMouseDownTime < 0.5f && Vector3.Distance(Input.mousePosition, rightMouseStartPosition) < 20f) {
            OnRightMouseQuickRelease?.Invoke();
        }

        /*
        if (Input.GetMouseButton(1)) {
            OnRightMouseDrag?.Invoke(rightMouseStartPosition);
        }
        */
    }

    /*
    private void Raycast() {
        var ray = CameraManager.Instance.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, 1000f);
        if (hits != null) { OnMouseRaycast?.Invoke(hits); }
    }
    */
}
