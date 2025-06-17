using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using UnityEngine;

public class SingleAttackHitBox : MonoBehaviour
{
    public int damage;

    [NonSerialized]
    public List<IDamageable> damagedEntities = new List<IDamageable>();

    private Collider[] hitboxColliders;

    private void Awake()
    {
        hitboxColliders = GetComponents<Collider>();
    }

    public void StartAttack()
    {
        foreach (Collider hitboxCollider in hitboxColliders)
        {
            hitboxCollider.enabled = true;
        }
    }

    public void FinishAttack()
    {
        foreach (Collider hitboxCollider in hitboxColliders)
        {
            hitboxCollider.enabled = true;
        }
        damagedEntities.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable) && !damagedEntities.Contains(damageable))
        {
            damageable.TakeDamage(damage, Vector3.zero, false);
            damagedEntities.Add(damageable);
        }
    }
}
