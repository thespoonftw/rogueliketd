using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 startMouse;
    private Vector3 startPosition;
    private List<Camera> cams;
    private float currentZoom;

    private void Start() {
        cams = GetComponentsInChildren<Camera>().ToList();
        var midpoint = (Constants.GAME_GRID_SIZE * Constants.BLOCK_SIZE) / 2f;
        transform.position = new Vector3(midpoint, 0, midpoint);
        currentZoom = cams[0].orthographicSize;
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            startMouse = Input.mousePosition;
            startPosition = transform.position;
        }
        if (Input.GetMouseButton(1)) {            
            var S = Input.mousePosition - startMouse;
            var yConst = -(currentZoom / Screen.height) * 2.8f;
            var xConst = yConst / 2f;
            var Wx = (yConst * S.y + xConst * S.x);
            var Wz = (yConst * S.y - xConst * S.x);
            transform.position = startPosition + new Vector3(Wx, 0, Wz);
            /*
            var currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePos.z = 0;
            var pos = transform.position + (startMouse - currentMousePos);
            transform.position = pos;
            startMouse
            */
        }

        var zoomVector = Input.mouseScrollDelta;
        currentZoom = Mathf.Clamp(currentZoom -= zoomVector.y, 1, 20);
        cams.ForEach(c => c.orthographicSize = currentZoom);
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
