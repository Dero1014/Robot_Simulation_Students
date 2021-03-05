using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : START
{
    void Start()
    {
        PutIn(transform);
    }

    void Update()
    {
        NewTrans.position = SelectMe();
        
    }
}
