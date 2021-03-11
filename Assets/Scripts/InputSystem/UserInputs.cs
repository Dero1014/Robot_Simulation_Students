using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputs2 : MonoBehaviour
{
    public static UserInputs2 current;

    public delegate void UserInput();
    public event UserInput CameraRotInput;
    public event UserInput FocusInput;
    public event UserInput CameraFocRotInput;
    public event UserInput CameraPanInput;
    public event UserInput CheckObjectInput;

    private void Awake()
    {
        current = this;
    }

    void CameraRotate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (CameraRotInput != null)
            {
                CameraRotInput();
            }
            if (Input.GetKey(KeyCode.F))
            {
                if (CameraFocRotInput != null)
                {
                    CameraFocRotInput();
                }
                
            }
        }
    }

    void Focus()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FocusInput != null)
            {
                FocusInput();
            }
        }
    }

    void Pan()
    {
        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (CameraPanInput != null)
            {
                CameraPanInput();
            }
        }
    }

    void CheckAObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CheckObjectInput != null)
            {
                CheckObjectInput();
            }
        }
    }

    void Update()
    {
        CameraRotate();
        Focus();
        Pan();
        CheckAObject();
    }
}
