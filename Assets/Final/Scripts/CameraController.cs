using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public CameraConnections currentConnection;
    public Cinemachine.CinemachineSmoothPath currentPath;
    public Cinemachine.CinemachineDollyCart dollyCart;
    public Rigidbody cameraRigidbody;
    public Cinemachine.CinemachineVirtualCamera targetCamera;
    private Cinemachine.CinemachineVirtualCamera tempCamera;
    public float LookAtDistance;
    public CameraConnections tempTarget;
    //intenal path data
    private CameraConnections destination;
    private float pathLength;
    private float pathPosition;
    private void Reset()
    {
        dollyCart = GetComponent<Cinemachine.CinemachineDollyCart>();
        cameraRigidbody = GetComponent<Rigidbody>();
        targetCamera = GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
    }
    private void Start()
    {
        //set inital path for testing purposes
        SetPath(currentConnection.paths[tempTarget], tempTarget);
        //set target camera to take priority over all others
        targetCamera.Priority = 11;
    }
    // Update is called once per frame
    void Update () {
        pathPosition = dollyCart.m_Position;
		if (pathPosition >= pathLength && destination != null)
        {
            //path complete
            currentConnection = destination;
            destination = null;
            tempCamera = currentConnection.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            //have the destination's camera take over
            tempCamera.Priority = 12;
        }
	}
    public void SetPath(Cinemachine.CinemachineSmoothPath path, CameraConnections newDestination)
    {
        if (tempCamera != null)
        {
            tempCamera.Priority = 10;
        }
        currentPath = path;
        destination = newDestination;
        pathLength = path.PathLength;
        //pathPosition = 0;
        dollyCart.m_Position = 0;
        dollyCart.m_Path = path;
        targetCamera.m_LookAt = newDestination.transform;
    }
	public bool CanTravelTo(CameraConnections connection)
	{
        return false;
		//if (
	}
}
