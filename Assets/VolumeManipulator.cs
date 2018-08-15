using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VolumeManipulator : MonoBehaviour {
    public float initalHealth = 100;
    public float currentHealth;
    public float maxVignetteIntensity = .364f;
    public delegate void ValueChange(float newHealth);
    public ValueChange healthChange;
    public PostProcessVolume processVolume;
	// Use this for initialization
	void Start () {
        currentHealth = initalHealth;
        healthChange += OnHealthChange;
        StartCoroutine(lowerHealth());
	}
	public IEnumerator lowerHealth()
    {
        while(currentHealth > 0)
        {
            currentHealth--;
            healthChange(currentHealth);
            yield return new WaitForSeconds(.5f);
        }
    }
    public void OnHealthChange(float currentHealth)
    {
        float health = Mathf.Clamp01(currentHealth / initalHealth);
        Vignette vin = processVolume.profile.GetSetting<Vignette>();
        vin.intensity.Override(Mathf.Lerp(maxVignetteIntensity, 0, health));
        DepthOfField dof = processVolume.profile.GetSetting<DepthOfField>();
        dof.focusDistance.Override(Mathf.Lerp(.1f, 4f, health));
        dof.aperture.Override(Mathf.Lerp(.1f, 7f, health));
    }
}
