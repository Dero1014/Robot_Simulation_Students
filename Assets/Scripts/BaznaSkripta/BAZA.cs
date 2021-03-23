using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAZA : MonoBehaviour
{
    public Transform NewTrans = null;
    public Vector3 ManipulateThis;
    public float Speed;
    public string MyName = "";
    bool _action = false;

    public Transform target;          // iz move toola
    public GameObject holder;

    public Transform[] graphic;

    [Space(10)]
    public LayerMask moveLayer;

    Vector3 distance = Vector3.zero;

    bool moveActive = false;
    bool xAxis = false;
    bool yAxis = false;
    bool zAxis = false;
    bool distanceChecked = false;       // iz move toola




    public void PutIn(Transform trans)
    {
        NewTrans = trans;
        MyName = NewTrans.name;
    }

    public Vector3 Select()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check for what axis has been selected
        {
            
            if (Physics.Raycast(ray, out hitObject, Mathf.Infinity, moveLayer))
            {
                
                if (hitObject.transform.tag == "Move Tool")
                {
                    Debug.Log("checkam");
                    moveActive = true;

                    if (hitObject.transform.name == "X")
                    {
                        xAxis = true;
                        Debug.Log("x odabrana");
                    }
                    else if (hitObject.transform.name == "Y")
                    {
                        yAxis = true;
                        Debug.Log("y odabrana");
                    }
                    else if (hitObject.transform.name == "Z")
                    { zAxis = true;
                      Debug.Log("z odabrana");
                    }

                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            { //IF HELD YOU CAN MOVE IT
                    Debug.Log("OtherSide");
                    ManipulateThis = MoveTool();
                    return ManipulateThis;
            }
            else //IF ITS NOT HELD THEN NOTHING IS PICKED
            {
                xAxis = false;
                yAxis = false;
                zAxis = false;
                moveActive = false;
                distanceChecked = false;
            }

            ChangeProperties();


            return ManipulateThis;

        }

        return ManipulateThis;


    }
    Vector3 MoveTool()
    {
        if (moveActive)
        {
            //move on X axis
            if (xAxis)
            {


                Vector3 mousePosition = GetMousePositionX(); //get the mouse position

                if (!distanceChecked) //set the difference between the mouse and the origin point
                {
                    distanceChecked = true;
                    distance = mousePosition - transform.position;
                }

                return (new Vector3(mousePosition.x - distance.x, transform.position.y, transform.position.z)); //apply the movement
            }

           

            if (yAxis)
            {



                Vector3 mousePosition = GetMousePositionY(); //get the mouse position

                if (!distanceChecked) //set the difference between the mouse and the origin point
                {
                    distanceChecked = true;
                    distance = mousePosition - transform.position;
                }

                return (new Vector3(transform.position.x, mousePosition.y - distance.y, transform.position.z)); //apply the movement
            }
            

            if (zAxis)
            {



                Vector3 mousePosition = GetMousePositionZ(); //get the mouse position

                if (!distanceChecked) //set the difference between the mouse and the origin point
                {
                    distanceChecked = true;
                    distance = mousePosition - transform.position;
                }

                return (new Vector3(transform.position.x, transform.position.y, mousePosition.z - distance.z)); //apply the movement
            }
            return (new Vector3(0, 0, 0));

        }
        return(new Vector3(0, 0, 0));
    }

    void ChangeProperties()
    {
        if (target != null)
        {
            transform.position = transform.position;
        }
    }

    #region mousePosition

    Vector3 pos;
    [Space(10)]
    int MaxAngle = 45;
    float _zAngle = 0;

    Vector3 _camAngle;

    Vector3 GetMousePositionX()
    {
        ////Use of planes to determain the position of the mouse relative to the axis we are using
        //Plane planeY = new Plane(Vector3.up, transform.position);
        //Plane planeZ = new Plane(Vector3.forward, transform.position);

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        ////for more effective results we use two planes that cover the parts that it needs to calculate for that axis 

        //float distanceToPlane;

        //if (planeY.Raycast(ray,out distanceToPlane))
        //    pos = ray.GetPoint(distanceToPlane);

        Plane planeX;

        _camAngle = transform.position - Camera.main.transform.position;

        _zAngle = Vector3.Angle(_camAngle, graphic[2].transform.forward);


        if (_zAngle >= MaxAngle) //POD ODREĐENIM KUTEM KORISTI Z ILI Y OS
        {
            planeX = new Plane(Vector3.forward, transform.position);
        }
        else
        {
            planeX = new Plane(Vector3.up, transform.position);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distanceToPlane;

        if (planeX.Raycast(ray, out distanceToPlane))
            pos = ray.GetPoint(distanceToPlane);

        return pos;
    }

    Vector3 GetMousePositionY()
    {
        //Use of planes to determain the position of the mouse relative to the axis we are using
        Plane planeY = new Plane(Vector3.forward, transform.position);
        Plane planeZ = new Plane(Vector3.right, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //for more effective results we use two planes that cover the parts that it needs to calculate for that axis 

        float distanceToPlane;

        if (planeY.Raycast(ray, out distanceToPlane))
            pos = ray.GetPoint(distanceToPlane);

        if (planeZ.Raycast(ray, out distanceToPlane))
            pos = ray.GetPoint(distanceToPlane);

        return pos;
    }

    Vector3 GetMousePositionZ()
    {
        //Use of planes to determain the position of the mouse relative to the axis we are using
        Plane planeZ = new Plane(Vector3.right, transform.position);
        Plane planeX = new Plane(Vector3.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //for more effective results we use two planes that cover the parts that it needs to calculate for that axis 

        float distanceToPlane;

        if (planeZ.Raycast(ray, out distanceToPlane))
            pos = ray.GetPoint(distanceToPlane);

        if (planeX.Raycast(ray, out distanceToPlane))
            pos = ray.GetPoint(distanceToPlane);

        return pos;
    }
    #endregion
}


