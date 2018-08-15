using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePent : MonoBehaviour {
    public List<vertexDistance> vertsInfo = new List<vertexDistance>();
    [System.Serializable]
    public struct vertexDistance
    {
        public Vector3 vertex;
        public float distance;
        public vertexDistance(Vector3 vertexInput, float distanceInput)
        {
            vertex = vertexInput;
            distance = distanceInput;
        }
    }
	// Use this for initialization
	void Start () {
        Debug.Log("Creating pentagon at " + transform.position);
        List<Vector3> verts = new List<Vector3>();
        GameObject vert = new GameObject();
        vert.transform.SetParent(transform);
        vert.transform.localPosition = new Vector3(1, 0, 0);
        for (int i = 0; i < 5; i++)
        {
            GameObject vert2 = Instantiate(vert, transform);
            vert2.transform.RotateAround(transform.position,  new Vector3(0, 1, 0), 360 / 5 * i);
            verts.Add(vert2.transform.position);
        }
        for (int i = 0; i < verts.Count; i++)
        {
            if (i > 1)
            {
                vertsInfo.Add(new vertexDistance(verts[i], Vector3.Distance(verts[i], verts[i - 1])));
                Debug.Log(verts[i] + " " + i +" is "+ (i-1) + " " + Vector3.Distance(verts[i], verts[i-1]));
            }
            else 
            {
                vertsInfo.Add(new vertexDistance(verts[0], Vector3.Distance(verts[0], verts[verts.Count - 1]))); 
                Debug.Log("i is 0 and max " + verts[0] + " is " + Vector3.Distance(verts[0], verts[verts.Count - 1]));
            }
        }
	}

}
