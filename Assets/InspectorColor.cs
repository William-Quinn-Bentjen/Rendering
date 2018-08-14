using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorColor : MonoBehaviour {
    public Color color;
    private MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
        meshRenderer.material.color = color;
    }
    
}
