using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendWalkToRun : MonoBehaviour {
    public Animator animator;
    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetFloat("Blend", animator.GetFloat("Blend") + .01f);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetFloat("Blend", animator.GetFloat("Blend") - .01f);
        }
    }
}
