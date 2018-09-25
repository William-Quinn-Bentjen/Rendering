using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pannel_Controller : MonoBehaviour {
    public static List<UI_Pannel_Controller> pannels = new List<UI_Pannel_Controller>();
    public CameraController cameraController;
    public CameraConnections connection;
    private void Start()
    {
        gameObject.SetActive(false);
        pannels.Add(this);
    }
    public void RoomOptionsCheck()
    {
        if (cameraController.currentConnection == connection)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public static void RoomOptionsCheckAll()
    {
        foreach(UI_Pannel_Controller pannel in pannels)
        {
            pannel.RoomOptionsCheck();
        }
    }
}
