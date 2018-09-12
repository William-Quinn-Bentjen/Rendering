using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConnections : MonoBehaviour
{
    public List<Cinemachine.CinemachineVirtualCamera> connectedTo = new List<Cinemachine.CinemachineVirtualCamera>();
    public Dictionary<Cinemachine.CinemachineVirtualCamera, Cinemachine.CinemachineSmoothPath> paths = new Dictionary<Cinemachine.CinemachineVirtualCamera, Cinemachine.CinemachineSmoothPath>();
    //public List<Cinemachine.CinemachineSmoothPath> pathComponents = new List<Cinemachine.CinemachineSmoothPath>();
    private static Cinemachine.CinemachineSmoothPath.Waypoint here = new Cinemachine.CinemachineSmoothPath.Waypoint { position = Vector3.zero };
    public void Connect()
    {
        //remove connections
        Disconnect();
        //create new paths for each camera connected
        for (int i = 0; i < connectedTo.Count; i++)
        {
            //create new path
            Cinemachine.CinemachineSmoothPath path = gameObject.AddComponent<Cinemachine.CinemachineSmoothPath>();
            //add waypoint from here to the connected camera
            path.m_Waypoints = new Cinemachine.CinemachineSmoothPath.Waypoint[2] { here, new Cinemachine.CinemachineSmoothPath.Waypoint { position = transform.InverseTransformPoint(connectedTo[i].transform.position) } };
            //add path to the dictionary
            paths.Add(connectedTo[i], path);
        }
    }
    public void Disconnect()
    {
        //destroy all paths on this gameObject
        Cinemachine.CinemachineSmoothPath[] pathComponents = GetComponents<Cinemachine.CinemachineSmoothPath>();
        for (int i = 0; i < pathComponents.Length; i++)
        {
            //destory path component 
            DestroyImmediate(pathComponents[i]);
        }
        //clear dictonary of paths
        paths.Clear();
    }
}
