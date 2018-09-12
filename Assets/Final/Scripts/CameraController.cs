using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public List<Cinemachine.CinemachineSmoothPath> targetPath = new List<Cinemachine.CinemachineSmoothPath>();
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
        }
        else if (pathPosition + LookAtDistance >= pathLength)
        {
            //stop camera look at
            //lerp rotation
        }
	}
    private void SetPath(Cinemachine.CinemachineSmoothPath path, CameraConnections endPoint)
    {
        dollyCart.m_Path = path;
        targetCamera.m_LookAt = endPoint.transform;
    }
}
