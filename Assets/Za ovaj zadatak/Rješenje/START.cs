using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class START : MonoBehaviour
{
    public Transform NewTrans = null;

    public Vector3 ManipulateThis;

    public float Speed;
    public string MyName = "";

    bool _action = false;

    public void PutIn(Transform trans)
    {
        NewTrans = trans;
        MyName = NewTrans.name;
    }

    // Update is called once per frame
    public Vector3 SelectMe()
    {
        if (Input.GetMouseButtonDown(0)) // pricekaj klik misa
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;
            
            if (Physics.Raycast(ray, out hitObject)) // pregledaj sto si lupio
            {
                if (hitObject.transform.gameObject.name == MyName) // ako je u tom layeru onda mozes obavljat akciju
                    _action = true;
            }
        }

        if (Input.GetMouseButton(0) && _action)
        {
            //ACTION
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0)
            {
                Vector3 temp = ManipulateThis; //nemozemo direktno mijenjati vrijednost x osi transforma pa spremamo u temp
                temp.x += (mouseX * Speed);
                ManipulateThis = temp;
            }
        }

        if (Input.GetMouseButtonUp(0)) //otpustis li mis gotovo je
            _action = false;

        return ManipulateThis;
    }
}
