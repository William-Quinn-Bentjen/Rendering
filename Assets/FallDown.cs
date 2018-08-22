using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour {
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            animator.SetTrigger("Damaged");
        }
    }
}
