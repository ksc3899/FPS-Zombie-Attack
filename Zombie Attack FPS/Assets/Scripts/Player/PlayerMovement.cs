using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float gravity = 20f;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = new Vector3(Input.GetAxis(Tags.HORIZONTAL), 0f, Input.GetAxis(Tags.VERTICAL));
        moveDirection = transform.TransformDirection(moveDirection);
        ApplyGravity();
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        PlayerJump();
        
        moveDirection.y = verticalVelocity;
    }

    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            verticalVelocity = jumpForce;
        }
    }
}
