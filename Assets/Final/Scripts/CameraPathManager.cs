using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//select all with class 
//folder generation/organize files
public class CameraPathManager : MonoBehaviour {
    public static List<CameraConnections> connectedCameras = new List<CameraConnections>();
    //not enough time to implement well

    public struct CameraPathData
    {
        public CameraConnections destination;
        public Queue<Cinemachine.CinemachineSmoothPath> path;
        public CameraPathData(CameraConnections destinationInput, Queue<Cinemachine.CinemachineSmoothPath> pathInput)
        {
            destination = destinationInput;
            path = pathInput;
        }
    };
    /*
public CameraPathData GetPath(CameraConnections start, CameraConnections end)
{
    //queue of paths that lead to the end
    Queue<Cinemachine.CinemachineSmoothPath> paths = new Queue<Cinemachine.CinemachineSmoothPath>();
    //list of centers that can be accessed by the paths
    List<CameraConnections> centerConnections = new List<CameraConnections>();
    //look at connections from the starting point
    foreach (CameraConnections startingConnection in start.connectedTo)
    {
        //if the starting point is diectly connected to the end point
        if (startingConnection.connectedTo.Contains(end))
        {
            //add path to the 
            paths.Enqueue(start.paths[end]);
            break;
        }
        //if it's connected to a center point add that to the list of connected center cameras
        else if (connectedCenterCameras.Contains(startingConnection))
        {
            centerConnections.Add(startingConnection);
        }
    }
    // the camera has found a center connection
    if (centerConnections.Count > 0)
    {
        List<CameraConnections> connectedToCenter = new List<CameraConnections>();
        foreach(CameraConnections center in centerConnections)
        {
            //if the starting point is directly connected to a center point that is directly connected to the ending point
            if (center.connectedTo.Contains(end))
            {
                paths.Enqueue(start.paths[center]);
                paths.Enqueue(center.paths[end]);
                return new CameraPathData(end, paths);
            }
        }
        // look for connections out of the center that will connect to the end point
    }
    else
    //the starting point is not directly connected to the ending point or a center point
    //THIS IS WHERE THE FUN STARTS!!!
    {

    }
    return new CameraPathData(end, paths);
}
*/
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
 public class EditModeFunctions : EditorWindow
{
    [MenuItem("Window/Connect Cameras")]
    public static void ShowWindow()
    {
        GetWindow<EditModeFunctions>("Connect Cameras");
        GetWindow<EditModeFunctions>("Disconnect Cameras");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Connect Cameras"))
        {
            ConnectCameras();
        }
        if (GUILayout.Button("Disconnect Cameras"))
        {
            DisconnectCameras();
        }
    }

    private void ConnectCameras()
    {
        Debug.Log("Connecting Cameras");
        DisconnectCameras();
        //CameraPathManager.connectedCameras.Clear();
        foreach (CameraConnections connection in FindObjectsOfType<CameraConnections>())
        {
            Debug.Log("Connected " + connection.name);
            CameraPathManager.connectedCameras.Add(connection);
            connection.Connect();
        }
    }
    private void DisconnectCameras()
    {
        Debug.Log("Disconnecting Cameras");
        CameraPathManager.connectedCameras.Clear();
        foreach (CameraConnections connection in FindObjectsOfType<CameraConnections>())
        {
            Debug.Log("Disconnected " + connection.name);
            connection.Disconnect();
        }
    }
}

