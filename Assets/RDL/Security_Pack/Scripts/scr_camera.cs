using UnityEngine;
using System.Collections;

public class scr_camera : MonoBehaviour {

    public float rotate_amount;
    public Vector3 Frequency = new Vector3(2,2,2);
    public Vector3 Amplitude = new Vector3(0,0,0);
    private Vector3 SineWaveRotationOffset;

	// Use this for initialization
	void Start () {
        SineWaveRotationOffset = transform.rotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (Mathf.Sin(Time.realtimeSinceStartup) * rotate_amount) + transform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 sineWaveRotation = SineWaveRotationOffset + new Vector3(Mathf.Sin(Time.fixedTime * Mathf.PI * Frequency.x) * Amplitude.x, Mathf.Sin(Time.fixedTime * Mathf.PI * Frequency.y) * Amplitude.y, Mathf.Sin(Time.fixedTime * Mathf.PI * Frequency.z) * Amplitude.z); ;
        //set rotation
        transform.eulerAngles = sineWaveRotation;
    }
}
