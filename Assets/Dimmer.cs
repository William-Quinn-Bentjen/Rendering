using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimmer : MonoBehaviour {
    private Light lightComponent;
    private float initalIntensity;
    private float timer;
	// Use this for initialization
	void Start () {
        lightComponent = GetComponent<Light>();
        initalIntensity = lightComponent.intensity;
        ChangeIntensity(100, 5);
	}
	public void ChangeIntensity(float newIntensity, float transistionTime)
    {
        StopAllCoroutines();
        initalIntensity = lightComponent.intensity;
        timer = 0;
        StartCoroutine(ChangeIntensityCoroutine(newIntensity, transistionTime));
    }
    private IEnumerator ChangeIntensityCoroutine(float newIntensity, float transistionTime)
    {
        while (timer < transistionTime)
        {
            timer += Time.deltaTime;
            lightComponent.intensity = Mathf.Lerp(initalIntensity, newIntensity, Mathf.Clamp01(timer / transistionTime));
            yield return new WaitForFixedUpdate();
        }
    }
	// Update is called once per frame
	void Update () {
	}
}
