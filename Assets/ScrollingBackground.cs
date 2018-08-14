using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    public float OffsetPerFrame = 10;
    public List<MoveRate> materials = new List<MoveRate>();
    [System.Serializable]
    public struct MoveRate
    {
        public Material material;
        [Range(0, 1)]
        public float moveRate;
        public MoveRate(Material mat, float matMoveRate = 1)
        {
            material = mat;
            moveRate = matMoveRate;
        }
    }
    public void Reset()
    {
        materials.Add (new MoveRate(GetComponent<MeshRenderer>().material));
        foreach (Transform child in transform)
        {
            Material temp = child.GetComponent<MeshRenderer>().material;
            if (temp != null)
            {
                materials.Add(new MoveRate(temp, .5f));
            }
        }
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		foreach(MoveRate move in materials)
        {
            move.material.mainTextureOffset += new Vector2(OffsetPerFrame * move.moveRate, 0);
        }
	}
}
