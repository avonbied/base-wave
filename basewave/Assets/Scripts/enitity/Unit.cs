using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    - {Target, WeaponRange, AttackRate, Damage}

*/
public class Unit : Entity {
    public Entity en;

    public void test() {
        bool temp = en.Dead;
        en.Dead = temp;
    }
}