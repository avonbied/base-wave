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

    Camera cam;

    [Header("Shop")]

    public int CostShotgun;
    public int CostMelee;
    public int CostLaser;
    public int CostPlasma;

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


    // Use this for initialization
    private void Awake()
    {
        Global.ProjectilePool = pool;
    }
    void Start()
    {
        Global.Controller = this;
        cam = Camera.main;
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



    Vector2 LastMousePos;
    private void Update()
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y, 5, 15);

        if (Input.GetMouseButtonDown(2))
            LastMousePos = Input.mousePosition;
        if (Input.GetMouseButton(2))
        {
            if (LastMousePos != (Vector2)Input.mousePosition)
            {
                Vector2 oldpos = cam.ScreenToWorldPoint(LastMousePos);
                Vector2 newpos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = (Vector2)cam.transform.position - (newpos - oldpos);
                pos.z = -10;
                cam.transform.position = pos;
            }
            LastMousePos = Input.mousePosition;
        }
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
