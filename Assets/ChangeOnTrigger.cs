using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeOnTrigger : MonoBehaviour {
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera triggerCamera;
    public int newPriority = 11;
    public bool isPermanent = false;
    private int oldPriority;
    private void OnTriggerEnter(Collider other)
    {
        oldPriority = triggerCamera.Priority;
        triggerCamera.Priority = newPriority;
    }
    private void OnTriggerExit(Collider other)
    {
        if (isPermanent == false)
        {
            triggerCamera.Priority = oldPriority;
        }
    }
}
