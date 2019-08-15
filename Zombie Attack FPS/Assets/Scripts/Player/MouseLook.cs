using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot;
    [SerializeField] private bool invert;
    [SerializeField] private bool canUnlock = true;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private int smoothSteps = 10;
    [SerializeField] private float smoothWeight = 0.4f;
    [SerializeField] private Vector2 lookLimits = new Vector2(-70, 80);
    private Vector2 lookAngle;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;
    private float currentRollAngle;
    private int lastLookFrame;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canUnlock)
        {
            LockAndUnlockCursor();
        }

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    private void LockAndUnlockCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(Tags.MOUSE_Y), Input.GetAxis(Tags.MOUSE_X));

        lookAngle.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f);
        lookAngle.x = Mathf.Clamp(lookAngle.x, lookLimits.x, lookLimits.y);
        lookAngle.y += currentMouseLook.y * sensitivity;

        playerRoot.localRotation = Quaternion.Euler(0f, lookAngle.y, 0f);
        lookRoot.localRotation = Quaternion.Euler(lookAngle.x, 0f, 0f);
    }
}
