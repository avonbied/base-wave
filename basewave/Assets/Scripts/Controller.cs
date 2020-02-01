using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public float MaxHP;
	public float _BaseHP;
	public float BaseHP { get { return _BaseHP; }  set { _BaseHP = value; rect.offsetMax = new Vector2(_BaseHP / MaxHP * 290f, rect.offsetMax.y); } }
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
	public RectTransform rect;


	// Use this for initialization
	void Start () {
		Global.Controller = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if (BaseHP > MaxHP) {
			BaseHP = MaxHP;
			Debug.LogError("You are murdering the cows!!");
		}
		if (BaseHP <= 0)
			Global.GameOver = true;
	}
}
