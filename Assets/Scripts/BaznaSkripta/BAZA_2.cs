using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAZA_2 : MonoBehaviour
{
    public Transform MainTransform = null;
    public Vector3 Value;

    public string NameTransform = "";

    public GameObject ParentContainer;

    public Transform[] Graphic;

    [Space(10)]
    public LayerMask ToolLayer;

    private Transform _targetObject;

    Vector3 _distance = Vector3.zero;

    Vector3 _mainValues;

    bool _rotActive = false;
    bool _moveActive = false;
    bool _xAxis = false;
    bool _yAxis = false;
    bool _zAxis = false;
    bool _distanceChecked = false;


    public void PutIn(Transform myTrans, Vector3 myProperty) //postaviti naš transform i njegovo ime
    {
        MainTransform = myTrans;
        NameTransform = MainTransform.name;
        ParentContainer = myTrans.parent.gameObject;
        _mainValues = myProperty;
        Value = _mainValues;
        print(_mainValues);
    }


    public void Main()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check for what axis has been selected
        {
            if (Physics.Raycast(ray, out hitObject, Mathf.Infinity, ToolLayer))
            {

                if (hitObject.transform.name == "X")
                    _xAxis = true;
                else if (hitObject.transform.name == "Y")
                    _yAxis = true;
                else if (hitObject.transform.name == "Z")
                    _zAxis = true;

                if (hitObject.transform.tag == "Move Tool")
                {
                    _moveActive = true;
                }
                else if (hitObject.transform.tag == "Rot Tool")
                {
                    _rotActive = true;
                }
                    
            }
        }
        if (Input.GetKey(KeyCode.Mouse0)) //IF HELD YOU CAN MOVE IT
            MoveTool();
        else
        {
            _xAxis = false;
            _yAxis = false;
            _zAxis = false;
            _moveActive = false;
            _rotActive = false; 
            _distanceChecked = false;
        }

        
    }

    public Transform FindTargetedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //casta ray
        RaycastHit hitObject;

        if (Physics.Raycast(ray, out hitObject, Mathf.Infinity, ~ToolLayer))
        {

            if (hitObject.transform.gameObject != null) //&& !hitObject.transform.GetComponentInParent<RobotCommands>() can add later
            {
                if (hitObject.transform.tag != "Ground")
                {
                    ParentContainer.SetActive(true);
                    _targetObject = hitObject.transform;

                    transform.position = _targetObject.position;
                }
                else
                {
                    ParentContainer.SetActive(false); //if it touches the ground
                    _targetObject = null;
                }

            }

            //if (hitObject.transform.GetComponentInParent<RobotCommands>())
            //{
            //    ParentContainer.SetActive(false);
            //    TargetObject = null;
            //}

        }
        else
        {
            ParentContainer.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _targetObject = null;
        }

        return _targetObject;

    }

    //yes
    void MoveTool()
    {
        if (_xAxis)
        {
            print(_mainValues);
            Vector3 mousePosition = GetMousePositionX(); //get the mouse position

            DistanceCheck(mousePosition);

            Value = new Vector3(mousePosition.x - _distance.x, _mainValues.y, _mainValues.z);
            print(_mainValues);
            //if (_moveActive)
            //    transform.position = new Vector3(mousePosition.x - _distance.x, transform.position.y, transform.position.z); //apply the movement
            //else if (_rotActive)
            //    transform.eulerAngles = new Vector3(mousePosition.x - _distance.x, transform.position.y, transform.position.z);
        }

        if (_yAxis)
        {


            Vector3 mousePosition = GetMousePositionY(); //get the mouse position

            DistanceCheck(mousePosition);

            //if (_moveActive)
            //    transform.position = new Vector3(transform.position.x, mousePosition.y - _distance.y, transform.position.z); //apply the movement
            //else if (_rotActive)
            //    transform.eulerAngles = new Vector3(transform.position.x, mousePosition.y - _distance.y, transform.position.z);

        }

        if (_zAxis)
        {

            Vector3 mousePosition = GetMousePositionZ(); //get the mouse position

            DistanceCheck(mousePosition);

            //if (_moveActive)
            //    transform.position = new Vector3(transform.position.x, transform.position.y, mousePosition.z - _distance.z); //apply the movement
            //else if (_rotActive)
            //    transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, mousePosition.z - _distance.z);

        }
       
        if (_targetObject != null)
        {
            if (_moveActive)
                _targetObject.position = transform.position;
            else if (_rotActive)
                _targetObject.eulerAngles = transform.eulerAngles;
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
        //Use of planes to determain the position of the mouse relative to the axis we are using
        Plane planeY = new Plane(Vector3.up, transform.position);
        Plane planeZ = new Plane(Vector3.forward, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //for more effective results we use two planes that cover the parts that it needs to calculate for that axis 

        float distanceToPlane;

        if (planeY.Raycast(ray, out distanceToPlane))
            pos = ray.GetPoint(distanceToPlane);

        if (planeZ.Raycast(ray, out distanceToPlane))
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








































    void DistanceCheck(Vector3 mousePosition)
    {
        if (!_distanceChecked) //set the difference between the mouse and the origin point
        {
            _distanceChecked = true;
            _distance = mousePosition - MainTransform.position;
        }
    }
}