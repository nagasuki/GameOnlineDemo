using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MoveCamera : NetworkBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private bool rotMouse = true;

    float xRotation = 0;
    float mouseX;
    float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (playerBody == null)
        {
            if (this.isLocalPlayer)
            {
                playerBody = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
            }
        }
        else
        {
            print("Founded!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isClient)
        {
            if (rotMouse)
            {
                LookAround();
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    rotMouse = false;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    rotMouse = true;
                    Cursor.visible = false;
                }
            }
        }
        else
        {
            rotMouse = false;
        }
    }

    void LookAround()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
