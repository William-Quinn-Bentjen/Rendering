using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTriganle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var filter = GetComponent<MeshFilter>();
		var mesh = new Mesh ();
		filter.mesh = mesh;
		var verts = new Vector3[4];
		verts [0] = new Vector3 (0, 0, 0);
		verts [1] = new Vector3 (1, 0, 0);
		verts [2] = new Vector3 (0, 1, 0);
		verts [3] = new Vector3 (1, 1, 0);
        mesh.vertices = verts;
		var indices = new int[6];
        //lower left
        indices[0] = 0;
        indices[1] = 2;
        indices[2] = 1;
        //upper right
        indices[3] = 2;
        indices[4] = 3;
        indices[5] = 1;
        mesh.triangles = indices;
        var norms = new Vector3[4];
        norms[0] = -Vector3.forward;
        norms[1] = -Vector3.forward;
        norms[2] = -Vector3.forward;
        norms[3] = -Vector3.forward;
        mesh.normals = norms;
        var uv = new Vector2[4];

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        mesh.uv = uv;
        //
    }
}
