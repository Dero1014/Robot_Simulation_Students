using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCube : START
{
  
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
