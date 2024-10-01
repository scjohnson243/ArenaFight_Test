using UnityEngine;

public class RangedUnit : Unit
{
    public float attackRange;         // Distance from which the unit can attack
    public float attackDamage;        // Damage per attack
    public float attackCooldown;      // Time between attacks
    public GameObject projectilePrefab;  // Reference to the projectile prefab

    private float lastAttackTime = 0;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();  // Use the inherited FindTarget method
        }

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            // Ranged unit can attack if within attackRange
            if (distanceToTarget <= attackRange)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    ShootProjectile(target);  // Shoot a projectile at the target
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // Move toward the target but stop when within attackRange
                Move(target.transform.position);  // Call the inherited Move method
            }
        }
    }
    private void ShootProjectile(Unit target)
    {
        if (projectilePrefab != null)
        {
            // Instantiate the projectile at the unit's position and rotation
            GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Debug.Log($"Projectile instantiated by {gameObject.name}.");

            // Get the Projectile component and initialize it with the target and damage
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Innitialize(target, attackDamage);
                Debug.Log($"Projectile initialized with target: {target.name} and damage: {attackDamage}.");
            }
            else
            {
                Debug.LogError("Projectile component is missing from the instantiated projectile prefab.");
            }
        }
        else
        {
            Debug.LogError("No projectilePrefab assigned to the RangedUnit.");
        }
    }


    public override void Attack(Unit target)
    {
        // Ranged units shoot projectiles, so Attack() will trigger the projectile instead of directly damaging the target
    }

    public override void Move(Vector3 targetPosition)
    {
        // Use the inherited NavMeshAgent from the base class
        
        if (Vector3.Distance(transform.position, targetPosition) > attackRange)
        {
            agent.isStopped = false;  // Allow the agent to move
            agent.SetDestination(targetPosition);
        }
        else
        {
            agent.isStopped = true;  // Stop the agent when within attack range
        }
    }
}
