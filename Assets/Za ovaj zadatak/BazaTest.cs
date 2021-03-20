using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazaTest : MonoBehaviour
{
    public ScaleCube SpeedS;
    public RotateCube SpeedR;
    public MoveCube SpeedM;

    public bool _action = false;

    private void Start()
    {
        SpeedS = GetComponent<ScaleCube>();
        SpeedR = GetComponent<RotateCube>();
        SpeedM = GetComponent<MoveCube>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && name=="ScaleCube")// pricekaj klik misa
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;

            if (Physics.Raycast(ray, out hitObject)) // pregledaj sto si lupio
            {
                if (hitObject.transform.gameObject.name == "ScaleCube") // ako je u tom layeru onda mozes obavljat akciju
                    _action = true;
                Debug.Log("Pain");

                if (hitObject.transform.gameObject.name == "RotateCube") // ako je u tom layeru onda mozes obavljat akciju
                    _action = true;
                Debug.Log("is");

                if (hitObject.transform.gameObject.name == "MoveCube") // ako je u tom layeru onda mozes obavljat akciju
                    _action = true;

                Debug.Log("life");
            }
        }

        if (Input.GetMouseButton(0) && _action)
        {
            //ACTION
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0)
            {
                Vector3 temp = transform.localScale; //nemozemo direktno mijenjati vrijednost x osi transforma pa spremamo u temp
                temp.x += (mouseX * SpeedS.Speed_Scal); // Hvala Bogu inače bi se ranio s fucking ravnalom
                transform.localScale = temp;
            }
        }

        if (Input.GetMouseButtonUp(0)) //otpustis li mis gotovo je
            _action = false;

    }



}
