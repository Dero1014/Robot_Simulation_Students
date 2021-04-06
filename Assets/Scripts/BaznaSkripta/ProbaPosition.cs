using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//zelim se definirat
//kliknem na objekt 
//objekt mi je target
//objekt manipuliram ----> //kliknem na svoje osi //mičem miš //mijenjam neku vrijednost
//manipuliram sebe
//objekt = ja

//bugs
//#1 Ako ne kliknemo na objekt move tool gasi umjesto da se nevidi
//#2 Move tool prilagodi se objektu, a ne obrnuto

    //pozeljno
    //pravilno extractamo vrijednost

public class ProbaPosition : BAZA_2
{
    public Transform TargetObject;


    void Awake()
    {
        PutIn(transform, transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && TargetObject == null) //Check for what axis has been selected
        {
            TargetObject = FindTargetedObject();
        }

        if (TargetObject != null)
        {
            //manipuliram neku vrijednost
            Main();
            transform.position = Value;        
            TargetObject.position = transform.position;
        }

       // Main();
    }
}
