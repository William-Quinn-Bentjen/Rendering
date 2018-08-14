using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Fade : MonoBehaviour {
    private MeshRenderer meshRenderer;
    private Color initalColor;
    private float timer;
    // Use this for initialization
    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        StartFade(5);
	}
	public void StartFade(float fadeTime)
    {
        StopAllCoroutines();
        timer = 0;
        initalColor = meshRenderer.material.color;
        StartCoroutine(PreformFade(fadeTime));
    }
    private IEnumerator PreformFade(float fadeTime)
    {
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            meshRenderer.material.color = new Color(initalColor.r, initalColor.g, initalColor.b, Mathf.Lerp(initalColor.a, 0, Mathf.Clamp01(timer/fadeTime)));
            yield return new WaitForFixedUpdate();
        }
    }
	// Update is called once per frame
	void Update () {
        
	}
}
