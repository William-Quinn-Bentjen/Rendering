using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
    public Rigidbody rb;
    public Vector3 velocity = new Vector3(0, 0, 2
        );
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	// Update is called once per frame
	void Update () {
        //i only want it to go a specific speed no matter what 
        rb.velocity = velocity;
	}
}
