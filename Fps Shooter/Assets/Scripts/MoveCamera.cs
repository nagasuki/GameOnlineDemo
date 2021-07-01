using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MoveCamera : NetworkBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    public GameObject CameraMountPoint;

    private bool visbleMouse = false;
    private bool rotMouse = true;

    float xRotation = 0;
    float mouseX;
    float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotMouse)
        {
            LookAround();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (visbleMouse == true)
            {
                rotMouse = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                visbleMouse = false;
            }
            else
            {
                rotMouse = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                visbleMouse = true;
            }

        }
    }

    void LookAround()
    {
        if (NetworkClient.localPlayer)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
