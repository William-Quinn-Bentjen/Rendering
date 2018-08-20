using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour {
    public Animator animator;
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            animator.SetInteger("State", 1);        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetInteger("State", 2);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetInteger("State", 3);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
        Debug.Log(animator.GetInteger("State"));
    }
}
