using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blend_Animation : MonoBehaviour {
	public Slider blendSlider;
	public Animator animator;
	public void ChangeValue()
	{
		//blendSlider.value
		animator.SetFloat("Blend", blendSlider.value);
	}
}
