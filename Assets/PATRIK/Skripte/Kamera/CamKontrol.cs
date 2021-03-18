using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamKontrol : MonoBehaviour
{   // ----------Zumiranje----------
    private Camera cam;
    private float startingFOV;

    public float minFOV = 1;
    public float maxFOV = 10;
    public float zoomRate=5;

    private float currentFOV;

    //------Rotacija-------
    float speedH = 2.0f;
    float speedV = 2.0f;
    float yaw = 0.0f;
    float pitch = 0.0f;

    //--------Kretanje--------
    Vector3 tempPos;
     

    void Start()
    {
        // ----------Zumiranje-----------
        cam = GetComponent<Camera>();
        startingFOV = cam.fieldOfView;

    } // - Zumiranje

    void Update()
    {
        // ----------Zumiranje---------
        UseWheel();
        //------Kretanje-----
        Kretanje();

    }
    private void LateUpdate()
    {
        //-------Rotacija------
        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    } // -Rotacija
    public void UseWheel()
    {
        currentFOV = cam.fieldOfView;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        currentFOV += scroll * zoomRate;

        currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        cam.fieldOfView = currentFOV;
    }
    public void Kretanje()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            tempPos = transform.position;
            tempPos.x += 1f;
            transform.position = tempPos;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            tempPos = transform.position;
            tempPos.x -= 1f;
            transform.position = tempPos;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            tempPos = transform.position;
            tempPos.z += 1f;
            transform.position = tempPos;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            tempPos = transform.position;
            tempPos.z -= 1f;
            transform.position = tempPos;
        }
    }
}
