using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 moveDirection;

    public float speed = 5f;
    private float gravity = 20f;

    public float jumbForce = 10f;
    private float verticalVelocity;

    void Awake()
    {
        // Assigning CharacterController
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calling the movement function to move the player
        Movement();
    }

    // Player movement
    void Movement()
    {
        // Movement controls (AWSD)
        moveDirection = new Vector3(Input.GetAxis(Axis.Horizontal), 0f, Input.GetAxis(Axis.Vertical));

        // Actual movement
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        // Calling ApplyGravity
        ApplyGravity();

        // Applying Unity's move function
        characterController.Move(moveDirection);
    }

    // Gravity
    void ApplyGravity()
    {
        // Applying gravity
        verticalVelocity -= gravity * Time.deltaTime;

        // Calling PlayerJump
        PlayerJump();

        // On which axis the jump is
        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    // Jumping
    void PlayerJump()
    {
        // Checking if the player is on the ground, if yes, applying jumpForce
        if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumbForce;
        }
    }
}
