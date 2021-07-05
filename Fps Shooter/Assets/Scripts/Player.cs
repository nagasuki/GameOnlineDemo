using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed = 2.0f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    public bool grounded;
    public GameObject CameraMountPoint;
    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 move;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        if (isLocalPlayer)
        {
            Transform cameraTransform = Camera.main.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab
            cameraTransform.parent = CameraMountPoint.transform;  //Make the camera a child of the mount point
            cameraTransform.position = CameraMountPoint.transform.position;  //Set position/rotation same as the mount point
            cameraTransform.rotation = CameraMountPoint.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (this.isLocalPlayer)
        {
            grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

            if (grounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            }

            velocity.y += gravityValue * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}
