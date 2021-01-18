using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseFocus : MonoBehaviour
{     // by Patrik
    public float MaxAngle;
    [Header("The Axis:")]
    public Transform xAxis;
    public Transform yAxis;
    public Transform zAxis;
    [Space]
    public Transform Cam;
    [Header("Angle Changes:")] 
    public float _angleX;
    public float _angleY;
    public float _angleZ;

    private Vector3 _CM;

    private void Start()
    {
        Cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        #region X-os
        _CM = Cam.position - xAxis.position;
        _angleX = Vector3.Angle(xAxis.right, _CM);

        if (_angleX > 90)
            _angleX = 180 - _angleX;
        if (_angleX < MaxAngle)
            xAxis.gameObject.SetActive(false);
        else
            xAxis.gameObject.SetActive(true);
        #endregion

        #region Y-os
        _CM = Cam.position - yAxis.position;
        _angleY = Vector3.Angle(yAxis.up, _CM);

        if (_angleY > 90)
            _angleY = 180 - _angleY;
        if (_angleY < MaxAngle)
            yAxis.gameObject.SetActive(false);
        else
            yAxis.gameObject.SetActive(true);
        #endregion

        #region Z-os

        _CM = Cam.position - zAxis.position;
        _angleZ = Vector3.Angle(zAxis.forward, _CM);

        if (_angleZ > 90)
            _angleZ = 180 - _angleZ;
        if (_angleZ < MaxAngle)
            zAxis.gameObject.SetActive(false);
        else
            zAxis.gameObject.SetActive(true);
        #endregion

    }

}
