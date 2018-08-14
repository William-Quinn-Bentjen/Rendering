using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePent : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Debug.Log("Creating pentagon at " + transform.position);
        List<Vector3> verts = new List<Vector3>();
        GameObject vert = new GameObject();
        vert.transform.SetParent(transform);
        vert.transform.localPosition = new Vector3(1, 0, 0);
        verts.Add(vert.transform.position);
        for (int i = 0; i < 5; i++)
        {
            GameObject vert2 = Instantiate(vert, transform);
            vert2.transform.RotateAround(transform.position,  new Vector3(0, 1, 0), 360 / 5 * i);
            verts.Add(vert2.transform.position);
        }
        foreach(Vector3 pos in verts)
        {
            Debug.Log(pos);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
