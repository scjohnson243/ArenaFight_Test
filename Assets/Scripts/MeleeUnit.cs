using System;
using UnityEngine;
using UnityEngine.AI;
public class MeleeUnit : Unit
{
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;
    private float lastAttackTime = 0;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        
     
    }


    private void Update()
    {
        if (target == null)
        {
            FindTarget(); // Find a new target if none is set
        }

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= attackRange)
            {
                agent.isStopped = true;
                
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack(target);
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }
        }
    }

    public override void Attack(Unit target)
    {
        if (target.team != this.team)
        {
            Debug.Log($"{gameObject.name} attacks {target.gameObject.name} for {attackDamage} damage.");
            target.TakeDamage(attackDamage);
        }
    }

}
