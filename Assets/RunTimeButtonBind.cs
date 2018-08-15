using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunTimeButtonBind : MonoBehaviour {
    public Button button;
    public void DebugLoggy()
    {
        Debug.Log("Loggy");
    }
    public void DebugLoggy2()
    {
        Debug.Log("Loggy 2");
    }
    public void DebugLoggy3()
    {
        Debug.Log("Loggy 3");
    }
	// Use this for initialization
	void Start () {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(DebugLoggy));
        }
        else if(rand == 1)
        {
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(DebugLoggy2));
        }
        else
        {
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(DebugLoggy3));
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
