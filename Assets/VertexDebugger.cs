using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexDebugger : MonoBehaviour {
    public bool isEnabled = true;
    public float vertexSize = .1f;
    public List<Color> vertexColors = new List<Color>(new Color[1] { new Color(1, 0, 0, .5f) });
    private void OnDrawGizmos()
    {
        if (isEnabled)
        {
            int i = 0;
            foreach (Vector3 vert in GetComponent<MeshFilter>().mesh.vertices)
            {
                Vector3 vertexPlace = Camera.main.WorldToScreenPoint(transform.TransformPoint(vert));
                if (i >= vertexColors.Count)
                {
                    Gizmos.DrawSphere(transform.TransformPoint(vert), vertexSize);
                }
                else
                {
                    Gizmos.color = vertexColors[i];
                    Gizmos.DrawSphere(transform.TransformPoint(vert), vertexSize);
                }
                drawString("Vertex # " + i, transform.TransformPoint(vert));
                i++;
            }
        }
    }
    public void Reset()
    {
        isEnabled = true;
        vertexSize = .1f;
        vertexColors = new List<Color>(new Color[1] { new Color(1, 0, 0, .5f) });
    }
    static public void drawString(string text, Vector3 worldPos, Color? colour = null)
    {
        UnityEditor.Handles.BeginGUI();

        var restoreColor = GUI.color;

        if (colour.HasValue) GUI.color = colour.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        if (view != null)
        {
            Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreColor;
                UnityEditor.Handles.EndGUI();
                return;
            }

            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
            GUI.color = restoreColor;
            UnityEditor.Handles.EndGUI();
        }

    }
}
