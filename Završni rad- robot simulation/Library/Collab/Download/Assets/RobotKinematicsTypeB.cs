using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we hold inverse kinematics in here 
public class RobotKinematicsTypeB : MonoBehaviour
{
    public float robotSpeed;

    public Segment_Script[] segments;
    public Transform[] joints;
    [Space(10)]public Transform target;

    private Vector3 origin;

    private Vector3[] segAngle =new Vector3[4];
    private Vector3[] jointAngle = new Vector3[4];
    private void Start()
    {

        for (int i = 0; i < segments.Length; i++)
        {
            segAngle[i] = segments[i].transform.localEulerAngles;
        }

        for (int i = 0; i < joints.Length; i++)
        {
            jointAngle[i] = joints[i].localEulerAngles;
        }

    }

    void Update()
    {
        origin = segments[segments.Length - 2].transform.position;
        if (target != null)
        {
            SegmentFollow();
            Apply();
            TargetTracking();
        }
    }

    Vector3 oldDir =Vector3.zero;

    #region InverseKinematic
    public void SegmentFollow()
    {
        float[] angleX = new float[segments.Length];

        //inverse
        for (int i = 0; i < segments.Length; ++i)
        {
            if (i==0)
            {
                Vector3 direction = target.position - segments[i].transform.position;

                if (direction != oldDir && direction != Vector3.zero)
                {

                    angleX[i] = Vector3.SignedAngle(segments[i].transform.up, direction, segments[3].transform.right);
                    segAngle[i].x += angleX[i];


                    segments[i].transform.localEulerAngles = segAngle[i];

                    direction = direction.normalized * segments[i].sLenght.localScale.y;
                    segments[i].transform.position = target.position - direction;

                    segments[i].transform.localPosition = new Vector3(0, segments[i].transform.localPosition.y, segments[i].transform.localPosition.z);

                }
                oldDir = direction;
            }

            if (i == segments.Length - 1) //talking to the base of Y rotation
            {
                Vector3 direction = target.position - segments[i].transform.position;

                if (direction != oldDir && direction != Vector3.zero)
                {
                    float yN = direction.y;
                    direction.y = 0;
                    float angle = Vector3.Angle(segments[i].transform.forward, direction);

                    if (direction!=Vector3.zero)
                    {
                        segments[i].transform.forward = direction.normalized;
                    }
                    
                    direction.y = yN;
                }
                oldDir = direction;
            }

            if (i != 0 && i!=segments.Length-1)
            {
                Vector3 direction = segments[i-1].transform.position - segments[i].transform.position;

                if (direction != oldDir && direction != Vector3.zero)
                {

                    angleX[i] = Vector3.SignedAngle(segments[i].transform.up, direction, segments[3].transform.right);
                    segAngle[i].x += angleX[i];


                    segments[i].transform.localEulerAngles = segAngle[i];

                    direction = direction.normalized * segments[i].sLenght.localScale.y;
                    segments[i].transform.position = segments[i-1].transform.position - direction;

                    if (i == 1)
                        segments[i].transform.localPosition = new Vector3(0, segments[i].transform.localPosition.y, segments[i].transform.localPosition.z);

                }
                oldDir = direction;
            }
        }

        //put it back
        for (int i = segments.Length - 2; i >= 0; --i)
        {
            if (i == segments.Length - 2)
            {
                segments[i].transform.position = origin;
            }

            if (i != segments.Length - 2 )
            {
                Vector3 direction = segments[i + 1].transform.position - segments[i].transform.position;

                if (direction != oldDir && direction != Vector3.zero)
                {
                    direction = direction.normalized * segments[i + 1].sLenght.localScale.y;
                    segments[i].transform.position = segments[i + 1].transform.position - direction;
                }
                oldDir = direction;
            }

        }

    }
    
    void Apply()
    {
        // MOVING THE BASE
        float angleBaseForward = Vector3.SignedAngle(joints[3].forward, segments[3].transform.forward, Vector3.up);
        float angleBaseBack = Vector3.SignedAngle(-joints[3].forward, segments[3].transform.forward, Vector3.up);

        if (Mathf.Abs(angleBaseForward)<Mathf.Abs(angleBaseBack) && Mathf.Abs(angleBaseForward) > 1)
            jointAngle[3].y += (robotSpeed * Time.deltaTime * Mathf.Sign(angleBaseForward));
        else if ((Mathf.Abs(angleBaseBack) < Mathf.Abs(angleBaseForward) && Mathf.Abs(angleBaseBack) > 1))
            jointAngle[3].y += (robotSpeed * Time.deltaTime * Mathf.Sign(angleBaseBack));

        if (Mathf.Abs(angleBaseForward) < 1)
            jointAngle[3].y += angleBaseForward;

        if (Mathf.Abs(angleBaseBack) < 1)
            jointAngle[3].y += angleBaseBack;

        joints[3].localEulerAngles = jointAngle[3];

        //MOVING ON X
        for (int i = 2; i > 0; i--)
        {
            float angleXUp = Vector3.SignedAngle(joints[i].up, segments[i].transform.up, joints[3].transform.right);


            if (Mathf.Abs(angleXUp) < 1)
                jointAngle[i].x += angleXUp;
            else if (angleXUp > 1 || angleXUp < -1)
                jointAngle[i].x += (robotSpeed * Time.deltaTime * Mathf.Sign(angleXUp));

            joints[i].localEulerAngles = jointAngle[i];
            
        }

        //do the hand 
        joints[0].transform.up = segments[0].transform.up;
    }
    #endregion

    #region Hand control
    private Transform targetGrab;
    private bool handClosed = false;
    public void CloseHand()
    {
        if (!handClosed)
        {
            handClosed = true;

            Collider[] col = Physics.OverlapSphere(joints[0].position, 1f, LayerMask.GetMask("Item"));
            print(col[0]);
            targetGrab = col[0].GetComponent<Transform>();
            targetGrab.parent = joints[0];
        }
    }

    public void OpenHand()
    {
        handClosed = false;
        if (targetGrab != null)
            targetGrab.parent = null;
    }

    #endregion


    //check how close it is to the targeted position
    [HideInInspector] public bool commandFulfilled = false;
    [HideInInspector] public bool track = false;
    void TargetTracking()
    {
        Vector3 distanceFromTarget = target.position - joints[0].position;
        if (distanceFromTarget.magnitude < 0.35f && track)
            commandFulfilled = true;
        else
            commandFulfilled = false;

    }

}
