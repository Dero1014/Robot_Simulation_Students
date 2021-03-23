using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputs : MonoBehaviour
{
    public static UserInputs current;

    public delegate void UserInput();
    public event UserInput CameraRotInput;
    public event UserInput FocusInput;
    public event UserInput CameraFocRotInput;
    public event UserInput CameraPanInput;
    public event UserInput CheckObjectInput;
    public event UserInput DeleteItemInput;
    public event UserInput PointsCreatorInput;

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

    void DeleteAnItem()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (DeleteItemInput != null)
            {
                DeleteItemInput();
            }

        }
    }

    void PointCreator()
    {
        if (Input.GetKeyDown("p") ) //jedino radi kad nije upaljena konzola, to treba dodat
        { 
            if(PointsCreatorInput != null)
            {
                PointsCreatorInput();
            }
        }
    }

    void Update()
    {
        PointCreator();
        DeleteAnItem();
        CameraRotate();
        Focus();
        Pan();
        CheckAObject();
    }
}
