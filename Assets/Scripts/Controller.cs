﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Controller : MonoBehaviour, IDamageable, IHealable
{

    public float MaxHP;
    public float _BaseHP;
    public float BaseHP { get { return _BaseHP; } set { _BaseHP = value; rect.offsetMax = new Vector2(_BaseHP / MaxHP * 290f, rect.offsetMax.y); } }
    public int BaseLevel;
    [SerializeField]
    private float _credits;
    public float Credits { get => _credits; set => _credits = Mathf.Clamp(value, 0, float.MaxValue); }
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

    [Header("Shop")]

    public int CostShotgun;
    public int CostMelee;
    public int CostLaser;
    public int CostPlasma;
    public int CostRepair;

    public GameObject PrefabShotgun;
    public GameObject PrefabLaser;
    public GameObject PrefabMelee;
    public GameObject PrefabPlasma;

    public void BuyRangedShotgun()
    {
        if (AttemptToBuy(CostShotgun))
            Instantiate(PrefabShotgun, Vector3.zero, new Quaternion());
    }
    public void BuyMelee()
    {
        if (AttemptToBuy(CostMelee))
            Instantiate(PrefabMelee, Vector3.zero, new Quaternion());
    }
    public void BuyRangedLaser()
    {
        if (AttemptToBuy(CostLaser))
            Instantiate(PrefabLaser, Vector3.zero, new Quaternion());
    }
    public void BuyRangedPlasma()
    {
        if (AttemptToBuy(CostPlasma))
            Instantiate(PrefabPlasma, Vector3.zero, new Quaternion());
    }

    int TimesUsed = 0;
    public void BuyRepairBase()
    {
        if (AttemptToBuy(CostRepair * (TimesUsed + 1)))
        {
            TimesUsed++;
            Global.Controller.BaseHP = Global.Controller.MaxHP;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        Global.ProjectilePool = pool;
        Global.UnitSpawner = GetComponent<UnitSpawner>();
        Global.TimeGameStarted = Time.realtimeSinceStartup;
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
            _credits -= Cost;
            return true;
        }
        return false;
    }
}
