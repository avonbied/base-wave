using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IDamageable, IHealable
{

    public float MaxHP;
    public float _BaseHP;
    public float BaseHP { get { return _BaseHP; } set { _BaseHP = value; rect.offsetMax = new Vector2(_BaseHP / MaxHP * 290f, rect.offsetMax.y); } }
    public int BaseLevel;
    public float Credits;
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

    public PoolManager pool;


    // Use this for initialization
    private void Awake()
    {
        Global.ProjectilePool = pool;
    }
    void Start()
    {
        Global.Controller = this;
    }

    private void FixedUpdate()
    {
        if (BaseHP > MaxHP)
        {
            BaseHP = MaxHP;
            Debug.LogError("You are murdering the cows!!");
        }
        if (BaseHP <= 0)
            Global.GameOver = true;
    }

    public void Hit(float DamagePoints)
    {
        BaseHP -= DamagePoints;
    }

    public void Heal(float HitPoints)
    {
        BaseHP += HitPoints;
    }


    public bool AttemptToBuy(float Cost)
    {
        if (Credits >= Cost)
        {
            Credits -= Cost;
            return true;
        }
        return false;
    }
}
