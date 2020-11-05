using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_HandTool_KeepDistance : MonoBehaviour
{
    public Transform handTool;
    public float maxDistance;

    private Vector3 direction;
    private float distance;
    private float difDis;

    void Update()
    {
        direction = handTool.position - transform.position;
        distance = direction.magnitude;
        direction = direction.normalized;
        print(distance);
        if (distance > maxDistance)
        {
            difDis = maxDistance - distance;
            handTool.localPosition += (direction * difDis);
        }
        

    }
}
