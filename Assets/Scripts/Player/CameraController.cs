using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameManager gameManager;

    public float mouseSensitivity = 50f;

    public Transform playerBody;

    private float rotationX = 0;

	void Start ()
    {
        gameManager = GameManager.instance;
	}
	
	void Update ()
    {
        if (gameManager.GetGameState() == GameState.Stop) { return; }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
