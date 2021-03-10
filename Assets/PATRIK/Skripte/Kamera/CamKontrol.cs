using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamKontrol : MonoBehaviour
{   // ----------Zumiranje----------
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    [SerializeField] private float zoomLerpSpeed = 10;
    //------Rotacija-------
    float speedH = 2.0f;
    float speedV = 2.0f;
    float yaw = 0.0f;
    float pitch = 0.0f;
  
     

    void Start()
    {
        // ----------Zumiranje-----------
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        // ----------Zumiranje-----------

    }

    void Update()
    {
        // ----------Zumiranje-----------
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 1.0f, 20f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        //------Kretanje-----
        if (Input.GetKeyDown(KeyCode.W))
        {
             
        }

    }
    private void LateUpdate()
    {
        //-------Rotacija------
        if (Input.GetMouseButton(0))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        //------Rotacija-------
    }
}
