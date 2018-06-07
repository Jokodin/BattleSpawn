using System.Collections.Generic;
using UnityEngine;
using ConstantData;

//Common functionality for all unit behavior.
public abstract class AttackingUnit : Unit, IKillable, IAttack
{
    //Unit-specific stats for combat and stuff.
    public float damage;
    public float attackSpeed;
    public float attackRange;
    public float sightRadius;

    //Stuff for functionality.
    public bool isAttacking;
    public bool isChasingEnemy;
    private float attackTimer;
    public PlayerObject enemyHQ;
    public PlayerObject target;
    

    new protected virtual void Start()
    {
        base.Start();

        damage = 1;
        attackSpeed = 1;

        sightRadius = 3;

        isAttacking = false;
        isChasingEnemy = false;
        attackTimer = attackSpeed;

        if (team.Equals(Constants.TEAM1))
        {
            enemyHQ = GameObject.Find("Team2HQ").GetComponent<PlayerObject>();
        }
        else if (team.Equals(Constants.TEAM2))
        {
            enemyHQ = GameObject.Find("Team1HQ").GetComponent<PlayerObject>(); ;
        }

        target = enemyHQ;

    }

    new protected virtual void FixedUpdate()
    {
        base.FixedUpdate();

        if (!target)
        {
            isChasingEnemy = false;
            isAttacking = false;
            target = enemyHQ;
            animator.SetBool("isAttacking", false);
        }

        agent.SetDestination(target.transform.position);

        if (isGrounded && !isAttacking)
        {
            if (IsTargetInAttackRange(target))
            {
                isAttacking = true;
                isChasingEnemy = false;
            }
        }

        //List of all enemy team entities
        List<PlayerObject> enemies = GameObject.Find("ManagerObject").GetComponent<ManagerObject>().getMyEnemies(team);
        if (!isAttacking)
        {
            float closestDistance = sightRadius;
            //Cycle through enemy entities and track their distance from this unit
            foreach (PlayerObject enemy in enemies)
            {
                float distanceToEnemy = getDistanceToTarget(enemy);
                if (IsTargetInSight(enemy) && distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    target = enemy;
                    isChasingEnemy = true;
                }
            }

        }
        
        if (isAttacking)
        {
            if (target)
            {
                
                animator.SetBool("isAttacking", true);
                attackTimer -= attackSpeed * Time.deltaTime;
                if (attackTimer <= 0)
                {
                    attackTimer = attackSpeed + Random.Range(0, 0.2F);
                    animator.SetTrigger("attackTrigger");
                    Attack(target, damage);
                }
            }
        }
        

    }

    public virtual void Attack(PlayerObject target, float damage)
    {
        //Debug.Log("Unit attacking");
        target.GetComponent<PlayerObject>().TakeDamage(GetComponent<PlayerObject>(), damage);
        
    }

    bool IsTargetInSight(PlayerObject target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= sightRadius) return true;
        return false;
    }

    bool IsTargetInAttackRange(PlayerObject target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= attackRange) return true;
        return false;
    }

    float getDistanceToTarget(PlayerObject target)
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }

}
