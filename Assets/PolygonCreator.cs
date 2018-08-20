using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonCreator : MonoBehaviour {
    public int sides = 3;
    public float extents = 1;
    public bool create = false;
    public static bool ValidCheck(int sides, float extents)
    {
        bool failed = false;
        string errorMessage = "";
        //needs sides to be 3 or more and extents to be positive
        if (sides < 3)
        {
            errorMessage += "Error: Sides must be greater than 2, sides = " + sides+ " ";
            failed = true;
        }
        if (extents <= 0)
        {
            errorMessage += "Error: Extents must be greater than 0, extents = " + extents;
            failed = true;
        }
        if (failed)
        {
            Debug.LogError(errorMessage);
            return false;
        }
        return true;
    }
    public static List<Vector3> GetCorners(int sides, float extents, Vector3 origin)
    {
        List<Vector3> retVal = new List<Vector3>();
        for (int i = 0; i < sides; i++)
        {
            float iValue = i * 2 * Mathf.PI / sides;
            retVal.Add(new Vector3(origin.x + extents * Mathf.Cos(iValue), origin.y, origin.z + extents * Mathf.Sin(iValue)));
        }
        return retVal;
    }
    public static List<Vector3> GetVertices(int sides, float extents, Vector3 origin)
    {
        List<Vector3> verts = GetCorners(sides, extents, origin);
        verts.Insert(0, origin);
        return verts;
    }
    public static List<int> GetTriangles(List<Vector3> verts)
    {
        List<int> retVal = new List<int>();
        for (int i = 0; i < verts.Count - 2; i++)
        {
            retVal.Add(i + 2);
            retVal.Add(i + 1);
            retVal.Add(0);
        }
        retVal.Add(1);
        retVal.Add(verts.Count - 1);
        retVal.Add(0);
        return retVal;
    }
    public static List<Vector3> GetNormals(int sides)
    {
        List<Vector3> retVal = new List<Vector3>();
        for(int i = 0; i <= sides; i++)
        {
            retVal.Add(-Vector3.forward);
        }
        return retVal;
    }
    public static List<Vector2> GetUVs(List<Vector3> verts)
    {
        List<Vector2> retVal = new List<Vector2>();
        for (int i = 0; i < verts.Count; i++)
        {
            retVal.Add(new Vector2(verts[i].x, verts[i].z));
        }
        return retVal;
    }
    public static GameObject GeneratePolygon(int sides, float extents, Vector3 position, Vector3 rotation, bool addDebugger = true)
    {
        if (ValidCheck(sides, extents))
        {
            GameObject retVal = new GameObject(sides + "-gon");
            MeshFilter filter = retVal.AddComponent<MeshFilter>();
            retVal.AddComponent<MeshRenderer>();
            Mesh mesh = new Mesh();
            filter.mesh = mesh;
            List<Vector3> vertices = GetVertices(sides, extents, position);
            mesh.vertices = vertices.ToArray();
            mesh.triangles = GetTriangles(vertices).ToArray();
            mesh.normals = GetNormals(sides).ToArray();
            mesh.uv = GetUVs(vertices).ToArray();
            if (addDebugger)
            {
                retVal.AddComponent<VertexDebugger>();
            }
            return retVal;
        }
        Debug.Log("Polygon creation aborted, invalid input");
        return null;
        
    }
    private void Start()
    {
        //GeneratePolygon(sides, extents, transform.position, transform.rotation.eulerAngles).AddComponent<VertexDebugger>();
    }
    private void Update()
    {
        if (create)
        {
            GeneratePolygon(sides, extents, transform.position, transform.rotation.eulerAngles);
            create = false;
        }
    }
}
