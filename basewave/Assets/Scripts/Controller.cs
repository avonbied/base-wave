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
            Debug.Log("hnmm");
            if (LastMousePos != (Vector2)Input.mousePosition)
            {
                Debug.Log("y u do dis :(");
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
