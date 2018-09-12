using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraPathManager : MonoBehaviour {

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
        foreach (CameraConnections connection in Component.FindObjectsOfType<CameraConnections>())
        {
            Debug.Log("Connected " + connection.name);
            connection.Connect();
        }
    }
    private void DisconnectCameras()
    {
        Debug.Log("Disconnecting Cameras");
        foreach (CameraConnections connection in Component.FindObjectsOfType<CameraConnections>())
        {
            Debug.Log("Disconnected " + connection.name);
            connection.Disconnect();
        }
    }
}

