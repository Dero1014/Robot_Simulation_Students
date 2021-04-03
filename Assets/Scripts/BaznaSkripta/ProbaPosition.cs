using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbaPosition : BAZA
{

    public static int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        PutIn(transform);
    }

    // Update is called once per frame
    void Update()
    {
        NewTrans.position = Select();
        Debug.Log("update");
        
    }
}
