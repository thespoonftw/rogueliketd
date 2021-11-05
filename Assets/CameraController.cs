using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 2f;
    private Camera camera;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    void Update() {
        var movementVector = new Vector2();
        var zoomVector = Input.mouseScrollDelta;

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

        var newZoom = Mathf.Clamp(camera.orthographicSize -= zoomVector.y, 1, 100);
        camera.orthographicSize = newZoom;

        transform.position = transform.position + (Vector3)movementVector * camera.orthographicSize;
    }
}
