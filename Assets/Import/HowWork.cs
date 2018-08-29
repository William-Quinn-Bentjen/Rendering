using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowWork : MonoBehaviour {
    public MeshRenderer renderer;
    private Texture2D texture;
    // Use this for initialization
    void Start()
    {
        texture = new Texture2D(512, 512);
        //mat.mainTexture = texture;
        StartCoroutine(Recalc());
    }

    private void Update()
    {
        //Debug.Log("Fixed Update");
        
    }

    IEnumerator Recalc()
    {
        while (true)
        {
            FastNoise myNoise = new FastNoise((int)Time.time); // Create a FastNoise object
            myNoise.SetNoiseType(FastNoise.NoiseType.Perlin); // Set the desired noise type
            float[,] heightMap = new float[texture.width, texture.height]; // 2D heightmap to create terrain


            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    //Debug.Log((int)(128 + myNoise.GetNoise(x, y) * 256));
                    //heightMap[x,y] = myNoise.GetNoise(x, y);
                    texture.SetPixel(x, y, new Color(1, 1, 1, 1));// / (int)(128 + myNoise.GetNoise(x, y) * 256)));
                }
            }
            texture.Apply();
            renderer.material.SetTexture("_TransTex", texture);
        }
    }
}
