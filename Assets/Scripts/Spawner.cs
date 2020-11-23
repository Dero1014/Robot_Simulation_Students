using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform objectsParent;

    [Space(10)]
    public GameObject item;
    public GameObject treadMill;
    public GameObject proxy;
    public GameObject laser;
    public GameObject robot;
    public GameObject handTool;
    public GameObject point;
    public RobotConsole robotConsole;

    [HideInInspector] public Transform clone;

    private Vector3 mPos;

    private int num = 0;
    private int numTread = 0;
    private int numL = 0;
    private int numP = 0;
    private int robRank = 0;

    private void Update()
    {
        Plane planeUp = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distanceToPlane;

        if (planeUp.Raycast(ray, out distanceToPlane))
            mPos = ray.GetPoint(distanceToPlane);

        if (clone != null)
            clone.position = mPos;

        if (Input.GetMouseButtonDown(0)) //press left click to cancel
            TryDestroyingExisting();

        if (Input.GetMouseButtonDown(1)) //press right click to confirm position
            clone = null;
    }

    private void TryDestroyingExisting()
    {
        if (clone != null)
            Destroy(clone.gameObject);
    }

    public void MakeItem()
    {
        TryDestroyingExisting();
        clone = Instantiate(item, mPos, Quaternion.identity, objectsParent).transform;
        clone.name = "Item " + num.ToString();
        num++;
    }

    public void MakeTreadMill()
    {
        TryDestroyingExisting();
        clone = Instantiate(treadMill, mPos, Quaternion.identity, objectsParent).transform;
        clone.name = "TreadMill " + numTread.ToString();
        numTread++;
    }

    public void MakeLaser()
    {
        TryDestroyingExisting();
        clone = Instantiate(laser, mPos, Quaternion.identity, objectsParent).transform;
        clone.name = "LASER" + numL.ToString();
        numL++;
    }

    public void MakeProxy()
    {
        TryDestroyingExisting();
        clone = Instantiate(proxy, mPos, Quaternion.identity, objectsParent).transform;
        clone.name = "PROXY" + numP.ToString();
        numP++;
    }

    public void MakeRobot()
    {
        TryDestroyingExisting();

        //spawn a robot
        GameObject cloneRobot = Instantiate(robot, mPos, Quaternion.identity, objectsParent);
        cloneRobot.name = "Robot " + robRank.ToString(); //name it
        clone = cloneRobot.GetComponent<Transform>(); //assign its transform
        cloneRobot.GetComponent<RobotManager>().rank = robRank; //set the rank
        OpenConsole opC = cloneRobot.GetComponent<OpenConsole>(); //get the OpenConsole script

        //assign its handtool
        GameObject cloneHandTool = Instantiate(handTool, new Vector3(clone.position.x, clone.position.y + 3.75f, clone.position.z), Quaternion.identity, clone);
        cloneHandTool.name = "Hand Tool " + robRank.ToString(); //name it
        cloneHandTool.GetComponent<MoveTool_Script>().holdToHand = true; //set it to control the hand
        cloneRobot.GetComponent<RobotKinematicsTypeB>().target = cloneHandTool.transform;  //set the target from the hand
        cloneHandTool.gameObject.SetActive(false); //set the handtool to false 
        cloneHandTool.AddComponent<PointsCreator>().point = point;
        cloneHandTool.GetComponent<PointsCreator>().rank = robRank;
        cloneRobot.GetComponentInChildren<ToggleMoveHand_Script>().moveTool = cloneHandTool; // give it the cloned hand tool specified for the robot
        opC.handTrans = cloneHandTool.transform;

        //assign its console
        opC.console = robotConsole;
        robotConsole.AddNewCodeSection();

        //stup the max distance
        cloneRobot.GetComponent<Robot_HandTool_KeepDistance>().maxDistance = 3f;
        cloneRobot.GetComponent<Robot_HandTool_KeepDistance>().minDistance = 0.5f;
        cloneRobot.GetComponent<Robot_HandTool_KeepDistance>().handTool = cloneHandTool.transform; //set the handtool

        robRank++;
    }
}
