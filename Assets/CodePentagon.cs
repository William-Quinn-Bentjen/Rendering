using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePentagon : MonoBehaviour {
    public static Vector3 origin = new Vector3(0, 0, 0);
    public static Vector3 vert0 = new Vector3(1, 0, 0);
    public static Vector3 vert1 = new Vector3(0.3f, 0, -1);
    public static Vector3 vert2 = new Vector3(-.8f, 0, -0.6f);
    public static Vector3 vert3 = new Vector3(-.8f, 0, .6f);
    public static Vector3 vert4 = new Vector3(.3f, 0, 1);
    // Use this for initialization
    void Start () {
        var filter = GetComponent<MeshFilter>();
        var mesh = new Mesh();
        filter.mesh = mesh;
        var verts = new Vector3[6] { origin, vert0, vert1, vert2, vert3, vert4 };
        mesh.vertices = verts;
        var indices = new int[15];
        //first face 0
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;
        //2nd face 1
        indices[3] = 0;
        indices[4] = 2;
        indices[5] = 3;
        //3nd face 2
        indices[6] = 0;
        indices[7] = 3;
        indices[8] = 4;
        //4nd face 3 
        indices[9] = 0;
        indices[10] = 4;
        indices[11] = 5;
        //5nd face 4
        indices[12] = 0;
        indices[13] = 5;
        indices[14] = 1;



        ////lower left
        //indices[0] = 0;
        //indices[1] = 2;
        //indices[2] = 1;
        ////upper right
        //indices[3] = 2;
        //indices[4] = 3;
        //indices[5] = 1;
        mesh.triangles = indices;
        var norms = new Vector3[6];
        norms[0] = -Vector3.forward;
        norms[1] = -Vector3.forward;
        norms[2] = -Vector3.forward;
        norms[3] = -Vector3.forward;
        norms[4] = -Vector3.forward;
        norms[5] = -Vector3.forward;
        mesh.normals = norms;
        var uv = new Vector2[6];

        uv[0] = new Vector2(origin.x, origin.z);
        uv[1] = new Vector2(vert0.x, vert0.z);
        uv[2] = new Vector2(vert1.x, vert1.z);
        uv[3] = new Vector2(vert2.x, vert2.z);
        uv[4] = new Vector2(vert3.x, vert3.z);
        uv[5] = new Vector2(vert4.x, vert4.z);

        mesh.uv = uv;
        foreach(Vector3 vert in verts)
        {
            Debug.Log("vertex at: " + transform.TransformPoint(vert));
        }
    }
}
