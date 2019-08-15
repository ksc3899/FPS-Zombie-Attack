using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;

    private PlayerMovement playerMovement;
    private Transform lookRoot;
    private float standHeight = 1.8f;
    private float crouchHeight = 1f;
    private bool isCrouching = false;
    private PlayerFootSteps playerFootSteps;
    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float minWalkVolume = 0.2f, maxWalkVolume = 0.6f;
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
        lookRoot = transform.GetChild(0);

        playerFootSteps.minVolume = minWalkVolume;
        playerFootSteps.maxVolume = maxWalkVolume;
        playerFootSteps.stepDistance = walkStepDistance;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            SprintStart();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            SprintEnd();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            CrouchStart();
        }
        else if(Input.GetKeyUp(KeyCode.C))
        {
            CrouchEnd();
        }
    }

    private void SprintStart()
    {
        playerMovement.speed = sprintSpeed;

        playerFootSteps.minVolume = sprintVolume;
        playerFootSteps.maxVolume = sprintVolume;
        playerFootSteps.stepDistance = sprintStepDistance;
    }

    private void SprintEnd()
    {
        playerMovement.speed = moveSpeed;

        playerFootSteps.minVolume = minWalkVolume;
        playerFootSteps.maxVolume = maxWalkVolume;
        playerFootSteps.stepDistance = walkStepDistance;
    }

    private void CrouchStart()
    {
        lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
        playerMovement.speed = crouchSpeed;

        playerFootSteps.minVolume = crouchVolume;
        playerFootSteps.maxVolume = crouchVolume;
        playerFootSteps.stepDistance = crouchStepDistance;

        isCrouching = true;
    }

    private void CrouchEnd()
    {
        lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
        playerMovement.speed = moveSpeed;

        playerFootSteps.minVolume = minWalkVolume;
        playerFootSteps.maxVolume = maxWalkVolume;
        playerFootSteps.stepDistance = walkStepDistance;

        isCrouching = false;
    }
}
