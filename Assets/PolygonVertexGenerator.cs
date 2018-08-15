using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonVertexGenerator : MonoBehaviour {
    public bool Recalculate = false;
    [Header("Input")]
    public int sides = 3;
    public float extents = 1;
    [Header("Debug info")]
    public List<Vector3> corners = new List<Vector3>();
    public List<float> distances = new List<float>();
    [Header("Gizmo Settings")]
    public Color originColor = Color.blue;
    public float originSize = .1f;
    public Color cornerColor = Color.green;
    public float cornerSize = .1f;
    public Color edgeColor = Color.red;

    /*
xi = r * cos(i * 2π/s)
yi = r * sin(i * 2π/s)
     */

    // Use this for initialization
    void Start () {
        corners = GetCorners(sides, extents, transform.position);
        distances = GetEdgeDistances(GetCorners(sides, extents, transform.position));
  //      origin = gameObject.transform.position;
		//for (int i = 0; i < sides; i++)
  //      {
  //          float iValue = i * 2 * Mathf.PI / sides;
  //          verts.Add(new Vector3(origin.x + extents * Mathf.Cos(iValue), 0, origin.z + extents * Mathf.Sin(iValue)));
  //      }
  //      for (int i = 0; i < verts.Count; i++)
  //      {
  //          if (i == 0)
  //          {
  //              distances.Add(Vector3.Distance(verts[0], verts[verts.Count - 1]));
  //          }
  //          else
  //          {
  //              distances.Add(Vector3.Distance(verts[i], verts[i - 1]));
  //          }
  //      }
        
	}
    private void Update()
    {
#if UNITY_EDITOR
        if (Recalculate)
        {
            Recalculate = false;
            corners = GetCorners(sides, extents, transform.position);
            distances = GetEdgeDistances(GetCorners(sides, extents, transform.position));
        }
#endif
    }
    public static List<Vector3> GetCorners(int sides, float extents, Vector3 origin)
    {
        List<Vector3> retVal = new List<Vector3>();
        if (sides < 3 || extents <= 0)
        {
            return retVal;
        }
        for (int i = 0; i < sides; i++)
        {
            float iValue = i * 2 * Mathf.PI / sides;
            retVal.Add(new Vector3(origin.x + extents * Mathf.Cos(iValue), 0, origin.z + extents * Mathf.Sin(iValue)));
        }
        return retVal;
    }
    public static List<Vector3> GetVerts(int sides, float extents, Vector3 origin)
    {
        List<Vector3> verts = GetCorners(sides, extents, origin);
        verts.Insert(0, origin);
        return verts;
    }
    public static List<float> GetEdgeDistances(List<Vector3> corners)
    {
        List<float> retVal = new List<float>();
        for (int i = 0; i < corners.Count; i++)
        {
            if (i == 0)
            {
                retVal.Add(Vector3.Distance(corners[0], corners[corners.Count - 1]));
            }
            else
            {
                retVal.Add(Vector3.Distance(corners[i], corners[i - 1]));
            }
        }
        return retVal;
    }
    private void OnDrawGizmos()
    {
        for(int i = 0; i < corners.Count; i++)
        {
            Gizmos.color = edgeColor;
            if (i == 0)
            {
                Gizmos.DrawLine(transform.TransformPoint(corners[0]), transform.TransformPoint(corners[corners.Count - 1]));
            }
            else
            {
                Gizmos.DrawLine(transform.TransformPoint(corners[i]), transform.TransformPoint(corners[i - 1]));
            }
            Gizmos.color = cornerColor;
            Gizmos.DrawSphere(transform.TransformPoint(corners[i]), cornerSize);
        }
        Gizmos.color = originColor;
        Gizmos.DrawSphere(transform.position, originSize);
    }
}
