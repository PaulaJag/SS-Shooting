using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstepSound;

    [SerializeField] private AudioClip[] footstepClip;

    private CharacterController characterController;

    [HideInInspector] public float volumeMin, volumeMax;

    // How far we can go before we play the sound (each step for a sound)
    private float accumulatedDistance;

    // How far we can go if sprinting, walking or crouching
    [HideInInspector] public float stepDistance;

    void Awake()
    {
        footstepSound = GetComponent<AudioSource>();

        characterController = GetComponentInParent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        // If we are not on the ground, don't execute else
        if (!characterController.isGrounded)
        {
            return;
        }
        
        // If valocity has any value, 
        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > stepDistance)
            {
                footstepSound.volume = Random.Range(volumeMin, volumeMax);
                footstepSound.clip = footstepClip[Random.Range(0, footstepClip.Length)];
                footstepSound.Play();

                accumulatedDistance = 0f;
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}
