using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZone : MonoBehaviour {
    public List<string> scenesToLoad = new List<string>();
    public List<string> scenesToUnload = new List<string>();
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            Loader.LoadScene(scenesToLoad[i]);
        }
        for (int i = 0; i < scenesToUnload.Count; i++)
        {
            Loader.UnloadScene(scenesToUnload[i]);
        }
    }
}
