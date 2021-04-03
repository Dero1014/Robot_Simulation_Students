using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 10f;
    
    private void Start()
    {
        gameObject.tag = "Crvenu";
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        }

    }
}  

