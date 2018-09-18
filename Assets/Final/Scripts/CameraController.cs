using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Queue<Cinemachine.CinemachineSmoothPath> currentPath = new Queue<Cinemachine.CinemachineSmoothPath>();
    private CameraConnections endPoint;
    public Cinemachine.CinemachineDollyCart dollyCart;
    public Cinemachine.CinemachineVirtualCamera targetCamera;
    public float LookAtDistance;
    private float pathLength;
    private float pathPosition;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        pathPosition = dollyCart.m_Position;
		if (pathPosition >= pathLength)
        {
            //next path
            NextPathSegment();
        }
        else if (pathPosition + LookAtDistance >= pathLength)
        {
            //stop camera look at
            //slerp rotation to next position
            if (currentPath.Count > 0)
            {
                Quaternion.Slerp(currentPath.Peek().transform.rotation, Quaternion.Euler(0, 0, 0), (pathLength - pathPosition) / LookAtDistance);
            }
            else
            {
                //Quaternion.Slerp(endPoint.transform.rotation, Quaternion.Euler(0, 0, 0));
            }
        }
	}
    private void NextPathSegment()
    {
        if (currentPath.Count > 0)
        {
            SetPathSegment(currentPath.Dequeue());
        }
        else
        {
            transform.rotation = endPoint.transform.rotation;
        }
    }
    private void SetPathSegment(Cinemachine.CinemachineSmoothPath path)
    {
        dollyCart.m_Position = 0;
        dollyCart.m_Path = path;
        if (currentPath.Count > 0)
        {
            targetCamera.m_LookAt = currentPath.Peek().transform;
        }
        else
        {
            targetCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void SetPath(CameraPathManager.CameraPathData pathData)
    {
        currentPath = pathData.path;
        NextPathSegment();
    }
}
