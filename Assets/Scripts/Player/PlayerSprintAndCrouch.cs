using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;

    private Transform lookRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;

    private bool isCrouching;

    // Footsteps
    private PlayerFootsteps playerFootsteps;

    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;

    private PlayerStats playerStats;

    private float sprintValue = 100f;
    private float sprintTreshold = 10f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        // Get the Child 0 of Player game object
        lookRoot = transform.GetChild(0);

        // Get player footsteps
        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();

        playerStats = GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // At the start, set the (normal) walking distance and walking volume min and max
        playerFootsteps.stepDistance = walkStepDistance;
        playerFootsteps.volumeMin = walkVolumeMin;
        playerFootsteps.volumeMax = walkVolumeMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Calling the Sprint and Crouch function
        Sprint();
        Crouch();
    }

    // Sprinting
    void Sprint()
    {
        // If we have stamina, we can sprint
        if (sprintValue > 0f)
        {
            // If -> If we press left shift while not crouching
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                // If -> If -> Apply sprint
                playerMovement.speed = sprintSpeed;

                // If -> If -> Set step distance to sprint step distance - Set volume min and max to sprint volume min and max
                playerFootsteps.stepDistance = sprintStepDistance;
                playerFootsteps.volumeMin = sprintVolume;
                playerFootsteps.volumeMax = sprintVolume;
            }
        }

        // If we release left shift while not crouching
        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            // If -> Remove sprint
            playerMovement.speed = moveSpeed;

            // If -> Set step distance to walking step distance - Set volume min and max to walking volume min and max
            playerFootsteps.stepDistance = walkStepDistance;
            playerFootsteps.volumeMin = walkVolumeMin;
            playerFootsteps.volumeMax = walkVolumeMax;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprintValue -= sprintTreshold * Time.deltaTime;

            // If -> If sprint value is less than or is 0
            if (sprintValue <= 0f)
            {
                sprintValue = 0f;

                // If -> If -> Reset player speed and volume
                playerMovement.speed = moveSpeed;
                playerFootsteps.stepDistance = walkStepDistance;
                playerFootsteps.volumeMin = walkVolumeMin;
                playerFootsteps.volumeMax = walkVolumeMax;
            }

            playerStats.DisplayStaminaStats(sprintValue);
        }
        else
        {
            if (sprintValue != 100f)
            {
                sprintValue += (sprintTreshold / 2f) * Time.deltaTime;

                playerStats.DisplayStaminaStats(sprintValue);

                if (sprintValue > 100)
                {
                    sprintValue = 100f;
                }
            }
        }
    }
    
    // Crouching
    void Crouch()
    {
        // If we press left control
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // If -> If we are crouching
            if (isCrouching)
            {
                // If If -> Set player speed to move speed
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = moveSpeed;

                // If If -> Set step distance to walking step distance - Set volume min and max to walking volume min and max
                playerFootsteps.stepDistance = walkStepDistance;
                playerFootsteps.volumeMin = walkVolumeMin;
                playerFootsteps.volumeMax = walkVolumeMax;

                //
                isCrouching = false;
            }
            // If -> If we are not crouching
            else
            {
                // If Else -> Set player speed to crouch speed
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;

                // If Else -> Set step distance to crouching step distance - Set volume min and max to crouching volume
                playerFootsteps.stepDistance = crouchStepDistance;
                playerFootsteps.volumeMin = crouchVolume;
                playerFootsteps.volumeMax = crouchVolume;

                //
                isCrouching = true;
            }
        }
    }
}
