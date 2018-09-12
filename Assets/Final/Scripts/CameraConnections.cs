using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConnections : MonoBehaviour
{
    public bool IsCenter = false;
    public List<CameraConnections> connectedTo = new List<CameraConnections>();
    public Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath> paths = new Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath>();
    private static Cinemachine.CinemachineSmoothPath.Waypoint here = new Cinemachine.CinemachineSmoothPath.Waypoint { position = Vector3.zero };
    public void Connect()
    {
        //remove connections
        Disconnect();
        //create new paths for each camera connected
        for (int i = 0; i < connectedTo.Count; i++)
        {
            if (connectedTo != null)
            {
                //create new path
                Cinemachine.CinemachineSmoothPath path = gameObject.AddComponent<Cinemachine.CinemachineSmoothPath>();
                //add waypoint from here to the connected camera
                path.m_Waypoints = new Cinemachine.CinemachineSmoothPath.Waypoint[2] { here, new Cinemachine.CinemachineSmoothPath.Waypoint { position = transform.InverseTransformPoint(connectedTo[i].transform.position) } };
                //add path to the dictionary
                paths.Add(connectedTo[i], path);
            }
            else
            {
                Debug.LogError("Can not have a camera connection with a null Connected To value", gameObject);
            }

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
