using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCube : START
{
    public float Speed_Scal =5;


    void Start()
    {
        PutIn(transform);
    }

    // Update is called once per frame
    void Update()
    {
        NewTrans.localScale= SelectMe();
    }
}
