using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseFocusOnAxis : MonoBehaviour
{
    public float MaxAngle;

    public Transform xAxis;
    public Transform yAxis;
    public Transform zAxis;

    public Transform cam;

    void Start()
    {
        
    }

    private float _angleX;
    private Vector3 _CM;
    void Update()
    {

        _CM = cam.position - xAxis.position;

        _angleX = Vector3.Angle(xAxis.right, _CM);

        if (_angleX > 90)
        {
            _angleX = 180 - _angleX;
        }
        print(_angleX);

        if (_angleX < MaxAngle)
        {
            //disable axis
            xAxis.gameObject.SetActive(false);

        }
        else
        {
            xAxis.gameObject.SetActive(true);
        }
    }
}
