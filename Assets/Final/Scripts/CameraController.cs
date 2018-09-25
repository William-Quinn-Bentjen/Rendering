using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public bool isMoving = false;
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
        //SetPath(currentConnection.paths[tempTarget], tempTarget);
        //set target camera to take priority over all others
        targetCamera.Priority = 11;
        isMoving = false;
    }
    // Update is called once per frame
    void Update () {
        pathPosition = dollyCart.m_Position;
		if (pathPosition >= pathLength && destination != null)
        {
            //path complete
            isMoving = false;
            currentConnection = destination;
            destination = null;
            tempCamera = currentConnection.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            //have the destination's camera take over
            tempCamera.Priority = 12;
            //tell UI to update (bad practice but crunch time)
            UI_Button_Controller.InteractableCheckAll();
            UI_Pannel_Controller.RoomOptionsCheckAll();
        }
	}
    public void SetPath(Cinemachine.CinemachineSmoothPath path, CameraConnections newDestination)
    {
        isMoving = true;
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
		if (isMoving == false && currentConnection.paths.ContainsKey(connection))
        {
            return true;
        }
        return false;
	}
    public void MoveTo(CameraConnections connection)
    {
        if (CanTravelTo(connection))
        {
            SetPath(currentConnection.paths[connection], connection);
        }
    }
}
