using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonVertexGenerator : MonoBehaviour {
    public float sides = 3;
    public float extents = 1;
    public Vector3 origin;
    public List<Vector3> verts = new List<Vector3>();
    public List<float> distances = new List<float>();

    /*
xi = r * cos(i * 2π/s)
yi = r * sin(i * 2π/s)
     */

    // Use this for initialization
    void Start () {
        origin = gameObject.transform.position;
		for (int i = 0; i < sides; i++)
        {
            float iValue = i * 2 * Mathf.PI / sides;
            verts.Add(new Vector3(origin.x + extents * Mathf.Cos(iValue), 0, origin.z + extents * Mathf.Sin(iValue)));
        }
        for (int i = 0; i < verts.Count; i++)
        {
            if (i == 0)
            {
                distances.Add(Vector3.Distance(verts[0], verts[verts.Count - 1]));
            }
            else
            {
                distances.Add(Vector3.Distance(verts[i], verts[i - 1]));
            }
        }
        
	}
    private void OnDrawGizmos()
    {
        foreach(Vector3 vert in verts)
        {
            Gizmos.DrawSphere(vert, .1f);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
