using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    public Teleporter Receiver;
    public void TeleportHere(Collider traveler)
    {
        ignoreList.Add(traveler);
        traveler.transform.position = transform.position;
        traveler.transform.rotation = transform.rotation;
    }
    List<Collider> ignoreList = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UnityChan" && ignoreList.Contains(other) == false)
        {
            Receiver.TeleportHere(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ignoreList.Remove(other);
    }
}
