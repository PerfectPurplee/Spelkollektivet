using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using UnityEngine;

public class SingleAttackHitBox : MonoBehaviour
{
    public int damage;
    public float multiplier = 1;
    public bool attackActive = false;
    public GameObject particlesToSpawn;

    public event Action<IDamageable> OnAttackEntity;

    [NonSerialized]
    public List<IDamageable> damagedEntities = new List<IDamageable>();

    private Collider[] hitboxColliders;

    private void Awake()
    {
        hitboxColliders = GetComponents<Collider>();
        foreach (Collider hitboxCollider in hitboxColliders)
        {
            hitboxCollider.enabled = false;
        }
    }

    public void StartAttack()
    {
        attackActive = true;
        foreach (Collider hitboxCollider in hitboxColliders)
        {
            hitboxCollider.enabled = true;
        }
    }

    public void FinishAttack()
    {
        attackActive = false;
        foreach (Collider hitboxCollider in hitboxColliders)
        {
            hitboxCollider.enabled = false;
        }
        damagedEntities.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hit something! : {other.gameObject.name}");
        if (other.TryGetComponent(out IDamageable damageable) && !damagedEntities.Contains(damageable))
        {
            Debug.Log($"Damaged something! : {other.gameObject.name}");
            damageable.TakeDamage(CalculateMultipliedDamage(), Vector3.zero, false);
            if (particlesToSpawn != null)
            {
                Instantiate(particlesToSpawn, other.transform.position, Quaternion.identity);
            }
            damagedEntities.Add(damageable);
            OnAttackEntity?.Invoke(damageable);
        }
    }

    public int CalculateMultipliedDamage()
    {
        return Mathf.FloorToInt(multiplier * damage);
    }
}
