using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabbedView : MonoBehaviour {
    public GameObject selectedPannel;
    public List<GameObject> pannels = new List<GameObject>();
    public void SelectPannel(GameObject pannel)
    {
        if (pannels.Contains(pannel) && selectedPannel != pannel)
        {
            if (selectedPannel != null)
            {
                selectedPannel.SetActive(false);
            }
            pannel.SetActive(true);
            selectedPannel = pannel;
        }
    }
	// Use this for initialization
	void Awake () {
		foreach(GameObject pannel in pannels)
        {
            pannel.SetActive(false);
        }
        if (selectedPannel != null)
        {
            selectedPannel.SetActive(true);
        }
	}
    private void Reset()
    {
        pannels.Clear();
        foreach(Transform pannel in transform)
        {
            pannels.Add(pannel.gameObject);
        }
        selectedPannel = null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
