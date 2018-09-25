using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button_Controller : MonoBehaviour {
    public static List<UI_Button_Controller> Button_Controllers = new List<UI_Button_Controller>();
    public CameraController controller;
    public CameraConnections connection;
    public Button button;
    private void Start()
    {
        Button_Controllers.Add(this);
    }
    public void InteractableCheck()
    {
        if (controller.CanTravelTo(connection))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
    public void MoveTo()
    {
        controller.MoveTo(connection);
        InteractableCheckAll();
    }
    public static void InteractableCheckAll()
    {
        foreach (UI_Button_Controller controller in Button_Controllers)
        {
            controller.InteractableCheck();
        }
    }

}
