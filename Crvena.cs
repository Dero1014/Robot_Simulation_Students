using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crvena : MonoBehaviour
{
    private GameObject target;
    void OnMouseOver()
    {
        target = GameObject.FindWithTag("Crvena");
        float xPos = target.transform.position.x;

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetAxis("Mouse X") < xPos)
            {
                transform.Rotate(0, 360 * Time.deltaTime, 0);
            }
            if (Input.GetAxis("Mouse X")> xPos)
            {
                target.transform.Rotate(0, -360 * Time.deltaTime, 0);
            }
        }
    }
}
