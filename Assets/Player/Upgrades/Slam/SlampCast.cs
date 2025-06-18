using Interface;
using UnityEngine;
using UnityEngine.VFX;

public class SlampCast : MonoBehaviour
{
    public LayerMask enemiesLayerMask;
    public GameObject slamEffectPrefab;
    public int damage;
    public float size;
    public float pushForce;

    public void CastOnPlayer()
    {
        Cast(Player.Player.Instance.transform.position);
    }

    public void Cast(Vector3 position)
    {
        Debug.Log("udalo sie");
        Instantiate(slamEffectPrefab, position+Vector3.up*0.1f, Quaternion.identity).GetComponent<VisualEffect>().Play();
        Collider[] enemiesHit = Physics.OverlapSphere(position, size, enemiesLayerMask);
        foreach (Collider enemyCollider in enemiesHit)
        {
            if (enemyCollider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage, Vector3.zero, false);
            }
            else
            {
                Debug.LogWarning($"{enemyCollider.name} ma enemy layer mask a nie ma idamageable");
            }

            if (pushForce != 0)
            {
                if (enemyCollider.TryGetComponent(out Rigidbody enemyRigidBody))
                {
                    enemyRigidBody.AddForce((enemyRigidBody.position - position).normalized * pushForce, ForceMode.VelocityChange);
                }
                else
                {
                    Debug.LogWarning($"{enemyCollider.name} ma enemy layer mask a nie ma rigidbody");
                }
            }
        }
    }
}
