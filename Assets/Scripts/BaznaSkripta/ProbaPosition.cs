using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbaPosition : BAZA_2
{

    // Start is called before the first frame update
    void Start()
    {
        PutIn(transform);
    }

    // Update is called once per frame
    void Update()
    {
        ShakeIt();
        
        
    }
}
