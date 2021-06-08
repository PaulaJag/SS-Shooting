using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot;

    [SerializeField] private bool invert;

    [SerializeField] private bool canUnlock = true;

    [SerializeField] private float sensivity = 5f;

    [SerializeField] private int smoothSteps = 10;

    [SerializeField] private float smoothWeight = 0.4f;

    [SerializeField] private float rollAngle = 10f;
    
    [SerializeField] private float rollSpeed = 3f;

    [SerializeField] private Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    private Vector2 lookAngles;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;
    private int lastLookFrame;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor to the center of the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Calling the function
        LockAndUnlockCursor();

        // When cursor is locked, we call LookAround function
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    // Locking and unlocking the cursor
    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    // Giving mouse the ability to move up/down and left/right
    void LookAround()
    {
        // Getting the axis
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MouseY), Input.GetAxis(MouseAxis.MouseX));

        // Applying movement for x and y, (Checking for invertivity)
        lookAngles.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensivity;

        // Clamping the value - Angle of looking can not go beyond default x and default y
        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

        // Lerping the value - Changing the value from currentRollAngle to Input*rollAngle linearly in specified time
        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MouseX) * rollAngle, Time.deltaTime * rollSpeed);

        // Look rotation is going up/down, while player rotation is going left/right
        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }
}
