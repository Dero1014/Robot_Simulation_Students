﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMoveHand_Script : MonoBehaviour
{
    public GameObject moveTool;

    public RobotManager roboMan;


    private void Start()
    {
        roboMan = GetComponentInParent<RobotManager>();
    }

    public bool moveToolActive = false;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Input.GetKeyDown(KeyCode.Mouse0) && moveTool != null)
        {

            if (Physics.Raycast(ray, out hitObject))
            {
               
                if (hitObject.transform.tag == "Hand")
                {
                    if (hitObject.transform.GetComponentInParent<RobotManager>().rank != roboMan.rank)
                    {
                        moveTool.SetActive(false);
                    }
                    else
                    {
                        moveToolActive = !moveToolActive;
                        moveTool.SetActive(moveToolActive);
                    }

                }

            }

        }

    }

}

