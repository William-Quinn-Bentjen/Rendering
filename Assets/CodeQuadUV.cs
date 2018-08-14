using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//costs at runtime
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CodeQuadUV : MonoBehaviour {
    public static Vector3[] verts = new Vector3[4]
    {
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(1,1,0),
        new Vector3(0,1,0),
    };
    public static int[] indices = new int[6]
    {
        2,1,0,
        3,2,0
    };
    public static Vector2[] uvs = new Vector2[4]
    {
        new Vector2(0,0),
        new Vector2(1,0),
        new Vector2(1,1),
        new Vector2(0,1)
    };
    public static Vector3[] norms = new Vector3[4]
    {
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward,
        -Vector3.forward
    };
	// Use this for initialization
	void Start () {
        var filter = GetComponent<MeshFilter>();
        var mesh = new Mesh();
        filter.mesh = mesh;
        mesh.vertices = verts;
        mesh.triangles = indices;
        mesh.uv = uvs;
        mesh.normals = norms;
    }
}
