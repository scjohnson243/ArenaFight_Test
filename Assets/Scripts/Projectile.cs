using System;
using Sirenix.OdinInspector.Editor.Modules;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;
    private Unit target;

    public void Innitialize(Unit unit, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if (target == null)
        {
            // If the target is gone (e.g., dead), destroy the projectile
            Destroy(gameObject);
            return;
        }
        
        
        // Move the projectile towards the target
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if the projectile is close enough to the target
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < 0.5f)  // You can adjust this threshold
        {
            HitTarget();
        }
    }

    // Called when the projectile hits the target
    private void HitTarget()
    {
        if (target != null)
        {
            // Deal damage to the target
            target.TakeDamage(damage);
        }
        // Destroy the projectile after hitting
        Destroy(gameObject);
    }
}
 