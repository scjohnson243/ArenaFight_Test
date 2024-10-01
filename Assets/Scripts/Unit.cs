using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {
    [SerializeField] public float health;
    [SerializeField] public float moveSpeed;
    [SerializeField] public Team team; // team assignment

    protected Unit target;  // Current target
    public abstract void Attack(Unit target);
    protected UnityEngine.AI.NavMeshAgent agent;  // For movement


    private void Awake()
    {
        // Ensure the NavMeshAgent component is properly assigned
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError($"No NavMeshAgent component found on {gameObject.name}");
        }
        else
        {
            agent.speed = moveSpeed;  // Ensure the speed is set correctly
        }
    }

    public virtual void Move(Vector3 targetPosition)
    {
        // Implement movement logic (e.g., Unity's NavMeshAgent, or simple Translate)
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    protected void FindTarget()
    {
        // Find all units in the scene
        Unit[] allUnits = FindObjectsOfType<Unit>();

        float closestDistance = Mathf.Infinity;
        Unit closestEnemy = null;

        // Loop through all units to find the closest enemy
        foreach (Unit unit in allUnits)
        {
            if (unit.team != this.team)  // Only consider enemies
            {
                float distance = Vector3.Distance(transform.position, unit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = unit;
                }
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy;  // Set the closest enemy as the target
        }
    }


    protected void Die()
    {
        Debug.Log($"{gameObject.name} has died");
        //this is where I would add death effects!!!

        Destroy(gameObject);
    }
    
    
}
