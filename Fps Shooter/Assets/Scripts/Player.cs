using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed = 2.0f;
    public bool grounded;
    private CharacterController controller;
    private Vector3 velocity;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
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
            grounded = controller.isGrounded;
            if (grounded && velocity.y < 0)
            {
                velocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * speed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            velocity.y += gravityValue * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
