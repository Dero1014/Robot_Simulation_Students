using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamKontrol : MonoBehaviour
{    // ----------Zumiranje----------
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    [SerializeField] private float zoomLerpSpeed = 10;
    // ----------Zumiranje-----------




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
        targetZoom = Mathf.Clamp(targetZoom, 1.5f, 20f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        // ----------Zumiranje-----------
    }
}
