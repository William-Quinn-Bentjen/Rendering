using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeCube : MonoBehaviour {
    public static Vector3[] verts = new Vector3[8]
    {
        //left
        new Vector3(.5f,.5f,.5f),
        new Vector3(.5f,-.5f,.5f),
        new Vector3(-.5f,.5f,.5f),
        new Vector3(-.5f,-.5f,.5f),
        //right
        new Vector3(.5f,.5f,-.5f),
        new Vector3(.5f,-.5f,-.5f),
        new Vector3(-.5f,.5f,-.5f),
        new Vector3(-.5f,-.5f,-.5f)
    };
    public static int[] indices = new int[36]
    {
        //left face 1
        2,1,0,
        //left face 2
        1,2,3,
        //right face 1
        4,5,6,
        //right face 2
        7,6,5,
        //front face 1
        3,2,7,
        //front face 2
        2,6,7,
        //back face 1
        0,1,5,
        //back face 2
        0,5,4,
        //top face 1
        4,2,0,
        //top face 2
        2,4,6,
        //bottom face 1
        3,7,1,
        //bottom face 2
        1,7,5

    };
    public static Vector3[] norms = new Vector3[12]
    {
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward
    };
    public static Vector2[] uvs = new Vector2[8]
    {
        new Vector2(0,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0),
        new Vector2(1,1),
        new Vector2(1,0),
        new Vector2(0,1),
        new Vector2(0,0)
    };
	// Use this for initialization
	void Start () {
        var filter = GetComponent<MeshFilter>();
        var mesh = new Mesh();
        filter.mesh = mesh;
        mesh.vertices = verts;
        mesh.triangles = indices;
        mesh.uv = uvs;
        //fix lighting
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
