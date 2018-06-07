using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInfantry : AttackingUnit, IAttack, IKillable{

    new void Awake()
    {
        moveSpeed = 5;
    }

    new protected virtual void Start()
    {
        //Debug.Log("SkeletonInfantry start");
        base.Start();

        objectName = "Skeleton Infantry";

        attackRange = 1.5F;
        maxHealth = 20;
        currentHealth = maxHealth;
    }

    override
    public void Attack(PlayerObject target, float damage)
    {
        //Debug.Log("SkeletonInfantry attacking");
        base.Attack(target, damage);
    }

    override
    public void TakeDamage(PlayerObject source, float damage)
    {
        //Debug.Log("SkeletonInfantry taking damage");
        base.TakeDamage(source, damage);
    }


}
