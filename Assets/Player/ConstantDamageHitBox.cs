using System.Collections.Generic;
using Interface;
using UnityEngine;

public class ConstantDamageHitBox : MonoBehaviour
{
    public int damagePerTick;
    public float tickDuration;

    private float nextTickTime;

    private List<IDamageable> entitiesInTrigger = new List<IDamageable>();

    private void FixedUpdate()
    {
        if (nextTickTime >= Time.time)
        {
            foreach (IDamageable entity in entitiesInTrigger)
            {
                entity.TakeDamage(damagePerTick, Vector3.zero, false);
            }
            nextTickTime += tickDuration;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable) && !entitiesInTrigger.Contains(damageable))
        {
            entitiesInTrigger.Add(damageable);
            damageable.OnDeath += () => entitiesInTrigger.Remove(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            entitiesInTrigger.Remove(damageable);
        }
    }
}
