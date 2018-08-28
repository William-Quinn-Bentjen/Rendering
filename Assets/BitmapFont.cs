using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitmapFont : MonoBehaviour {
    //public Sprite font;
    //public Image font;
    public Vector2Int tiles;
    public Vector2Int selected;
    public bool update = false;
    private Vector2 tileSize;
    private Mesh mesh;
    private MeshFilter meshFilter;
    // Use this for initialization
    void Start () {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        tileSize = new Vector2(1 / (float)tiles.x, 1 / (float)tiles.y);
        UpdateUVs();
	}
	
	// Update is called once per frame
	void Update () {
		if (update)
        {
            update = false;
            UpdateUVs();
        }
	}
    public void UpdateUVs()
    {
        if (selected.x < tiles.x && selected.y < tiles.y)
        {
            Vector2 selection = new Vector2(selected.x, selected.y);
            List<Vector2> uvs = new List<Vector2>();
            uvs.Add(new Vector2(selection.x * tileSize.x, selection.y * tileSize.y));
            uvs.Add(new Vector2((selection.x + 1f) * tileSize.x, (selection.y + 1f) * tileSize.y));
            uvs.Add(new Vector2((selection.x + 1f) * tileSize.x, selection.y * tileSize.y));
            uvs.Add(new Vector2(selection.x * tileSize.x, (selection.y + 1f) * tileSize.y));
            mesh.uv = uvs.ToArray();
                //    //botleft
                //    new Vector2(0, 0),
                //    //top right
                //    new Vector2(1, 1),
                //    //bot right
                //    new Vector2(1,0),
                //    //top left
                //    new Vector2(0,1)
        }
    }
}
