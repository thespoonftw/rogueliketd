using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 startMouse;
    private Camera cam;

    private void Start() {
        cam = GetComponentInChildren<Camera>();
        var midpoint = (Constants.GAME_GRID_SIZE * Constants.BLOCK_SIZE) / 2f;
        transform.position = new Vector3(midpoint, 0, midpoint);
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            startMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startMouse.z = 0;
        }
        if (Input.GetMouseButton(1)) {
            var currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePos.z = 0;
            var pos = transform.position + (startMouse - currentMousePos);
            transform.position = pos;
        }

        var zoomVector = Input.mouseScrollDelta;
        var newZoom = Mathf.Clamp(cam.orthographicSize -= zoomVector.y, 1, 100);
        cam.orthographicSize = newZoom;
    }

    /* old keyboard controls
        var movementVector = new Vector2();
        if (Input.GetKey(KeyCode.W)) {
            movementVector.y += MOVEMENT_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            movementVector.y -= MOVEMENT_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            movementVector.x += MOVEMENT_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            movementVector.x -= MOVEMENT_SPEED * Time.deltaTime;
        }
        transform.position = transform.position + (Vector3)movementVector * cam.orthographicSize;
        */
}
