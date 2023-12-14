using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : ActorManagerInterface
{
    private CapsuleCollider defCol;

    void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up * 1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Weapon")
        {
            am.TryDoDamage();
        }
    }
}