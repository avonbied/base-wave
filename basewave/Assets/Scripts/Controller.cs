using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public float BaseHP;
	public int BaseLevel;
	public int Credits;
	public int Wave;
	[Header("Modifiers")]
	public float RangedDmgModifier;
	public float RangedHPModifier;
	public float MeleeDmgModifier;
	public float MeleeHPModifier;
	public float RangedRangeModifier;
	public float RangedFireRateModifier;
	public float MeleeSpeedModifier;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if (BaseHP <= 0)
			Global.GameOver = true;
	}
}
