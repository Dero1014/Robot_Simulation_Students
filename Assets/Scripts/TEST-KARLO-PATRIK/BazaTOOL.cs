using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazaTOOL : MonoBehaviour
{
    public Transform NewTrans = null;
    public Vector3 ManipulateThis;

    public float Speed; // - treba izbaciti jer treba gledali brzinu miša
    public string MyTag = "";

    bool _action = false;

    /// <summary>
    /// //////////////////////////////
    /// </summary>
    public Transform target;
    public GameObject holder;
    public bool holdToHand;

    public Transform[] graphic;

    [Space(10)]
    public LayerMask moveLayer;

    Vector3 distance = Vector3.zero;

    bool moveActive = false;
    bool xAxis = false;
    bool yAxis = false;
    bool zAxis = false;
    bool distanceChecked = false;

    public void PutIn(Transform trans)
    {
        NewTrans = trans;
        MyTag = NewTrans.tag;
    }


    public Vector3 SelectMe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.transform.gameObject.tag == "Move Tool")
                    _action = true;
                if (hitObject.transform.name == "X")
                    xAxis = true;
                else if (hitObject.transform.name == "Y")
                    yAxis = true;
                else if (hitObject.transform.name == "Z")
                    zAxis = true;
            }
        }

        if (Input.GetMouseButton(0) && _action)
        {
            //ACTION
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0)
            {
                Vector3 temp = ManipulateThis;
                temp.x += (mouseX * Speed);
                ManipulateThis = temp;
            }
        }

        if (Input.GetMouseButtonUp(0))
            _action = false;

        return ManipulateThis;
    }
    void FindTargetedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check for what axis has been selected
        {
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.transform.gameObject != null && hitObject.transform.name != "X" && hitObject.transform.name != "Y" && hitObject.transform.name != "Z" && !hitObject.transform.GetComponentInParent<RobotCommands>())
                {
                    if (hitObject.transform.tag != "Ground")
                    {
                        holder.SetActive(true);
                        target = hitObject.transform;
                        transform.position = target.position;
                    }
                    else
                    {
                        holder.SetActive(false); //if it touches the ground
                        target = null;
                    }

                }

                if (hitObject.transform.GetComponentInParent<RobotCommands>())
                {
                    holder.SetActive(false);
                    target = null;
                }

            }
            else
            {
                holder.SetActive(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                target = null;
            }

        }
    }
    void MoveTool()
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

                transform.position = new Vector3(mousePosition.x - distance.x, transform.position.y, transform.position.z); //apply the movement
            }

            if (yAxis)
            {



                Vector3 mousePosition = GetMousePositionY(); //get the mouse position

                if (!distanceChecked) //set the difference between the mouse and the origin point
                {
                    distanceChecked = true;
                    distance = mousePosition - transform.position;
                }

                transform.position = new Vector3(transform.position.x, mousePosition.y - distance.y, transform.position.z); //apply the movement
            }

            if (zAxis)
            {



                Vector3 mousePosition = GetMousePositionZ(); //get the mouse position

                if (!distanceChecked) //set the difference between the mouse and the origin point
                {
                    distanceChecked = true;
                    distance = mousePosition - transform.position;
                }

                transform.position = new Vector3(transform.position.x, transform.position.y, mousePosition.z - distance.z); //apply the movement
            }

        }
    }

    Vector3 pos;
    [Space(10)]
    int MaxAngle = 45;
    float _zAngle = 0;

    Vector3 _camAngle;

    Vector3 GetMousePositionX()
    {
        
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
}
