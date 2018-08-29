using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
    public Vector3 Frequency = new Vector3(2, 2, 2);
    public Vector3 Amplitude = new Vector3(0, 0, 0);
    private Vector3 SineWaveRotationOffset;
    // Use this for initialization
    void Start()
    {
        SineWaveRotationOffset = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sineWaveRotation = SineWaveRotationOffset + new Vector3(Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * Frequency.x) * Amplitude.x, Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * Frequency.y) * Amplitude.y, Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * Frequency.z) * Amplitude.z); ;
        //set rotation
        transform.eulerAngles = sineWaveRotation;
    }
}
