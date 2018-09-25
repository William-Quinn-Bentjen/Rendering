using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour {
	public Animator attacker;
	public Transform attackerTrans;
	public Animator victim;
	public FallDown victimFallDown;
	public Transform victimTrans;

	private Vector3 attackerStartingPos;
	private Vector3 victimStartingPos;
	private Quaternion attackerStartingRot;
	private Quaternion victimStartingRot;
	// Use this for initialization
	void Start () {
	}
	public void ResetAnimation()
	{
		//victim.SetTrigger ("Reset");
		//victimTrans.position = victimStartingPos;
		//victimTrans.rotation = victimStartingRot;

		////attackerTrans.position = attackerStartingPos;
		////attackerTrans.rotation = attackerStartingRot;
	}
	public void Attack()
	{
		attacker.SetTrigger ("Punch");
	}
}
