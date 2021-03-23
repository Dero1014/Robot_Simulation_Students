using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToolNEW : BazaTOOL
{
    private void Start()
    {
        PutIn(transform);
    }
    void Update()
    {
        NewTrans.position = SelectMe(); 
    }

   
}
