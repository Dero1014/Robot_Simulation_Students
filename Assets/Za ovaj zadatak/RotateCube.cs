using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : START
{
    public float Speed_Rot;


    void Start()
    {
        PutIn(transform);
    }

    // Update is called once per frame
    void Update()
    {
        NewTrans.localEulerAngles = SelectMe();
    }
}
