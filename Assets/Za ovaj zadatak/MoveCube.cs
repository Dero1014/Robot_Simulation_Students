using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float Speed;

    bool _action = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // pricekaj klik misa
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;

            if (Physics.Raycast(ray, out hitObject)) // pregledaj sto si lupio
            {
                if (hitObject.transform.gameObject.name == "MoveCube") // ako je u tom layeru onda mozes obavljat akciju
                    _action = true;   
            }
        }

        if (Input.GetMouseButton(0) && _action)
        {
            //ACTION
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0)
            {
                Vector3 temp = transform.position; //nemozemo direktno mijenjati vrijednost x osi transforma pa spremamo u temp
                temp.x += (mouseX * Speed);
                transform.position = temp;
            }
        }

        if (Input.GetMouseButtonUp(0)) //otpustis li mis gotovo je
            _action = false;

    }
}
