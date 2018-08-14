using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightFocus : MonoBehaviour {
    private Light spotLight;
    public float maxIntensity;
    public float minIntensity;

    // Use this for initialization
    void Start () {
        spotLight = GetComponent<Light>();
	}
    private void FixedUpdate()
    {
        if (spotLight.spotAngle == 1)
        {
            spotLight.intensity = minIntensity;
        }
        else
        {
            spotLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Clamp01(1 - (spotLight.spotAngle / 179/*max spot angle value*/)));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
