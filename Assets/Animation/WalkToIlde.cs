using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToIlde : MonoBehaviour {

    public Animator animator;
    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetFloat("Forward", 1);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetFloat("Forward", 0);
        }
        else
        {
            animator.SetFloat("Forward", 0);
        }
        Debug.Log(animator.GetFloat("Forward"));
    }
}
