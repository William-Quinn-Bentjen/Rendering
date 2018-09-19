using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveEmission : MonoBehaviour {
    public Material targetMaterial;
    public Color EmissionColor;
    public float frequency = 1;
    public float maxIntensity = 1;
    // Update is called once per frame
    void Update () {
		if (targetMaterial != null)
        {
            targetMaterial.SetColor("_EmissionColor", EmissionColor * ((Mathf.Sin(Time.timeSinceLevelLoad * frequency) / 2 + .5f) * maxIntensity));
        }
	}
}
