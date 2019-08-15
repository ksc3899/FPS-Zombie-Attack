using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    [HideInInspector] public float maxVolume, minVolume;
    [HideInInspector] public float stepDistance;

    [SerializeField] private AudioClip[] footStepClips;
    private CharacterController characterController;
    private AudioSource footStepSound;
    private float accumulatedDistance = 0f;

    private void Start()
    {
        footStepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        if (!characterController.isGrounded)
            return;

        CheckToPlayFootStepSound();
    }

    private void CheckToPlayFootStepSound()
    {
        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistance)
            {
                footStepSound.volume = Random.Range(minVolume, maxVolume);
                footStepSound.clip = footStepClips[Random.Range(0, footStepClips.Length)];
                footStepSound.Play();

                accumulatedDistance = 0f;
            }
        }
        else
            accumulatedDistance = 0f;
    }
}
