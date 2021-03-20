using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FINISH : START
{
    // Start is called before the first frame update
    void Start()
    {
        PutIn(transform);
    }

    // Update is called once per frame
    void Update()
    {
        NewTrans.position = SelectMe();
    }
}
