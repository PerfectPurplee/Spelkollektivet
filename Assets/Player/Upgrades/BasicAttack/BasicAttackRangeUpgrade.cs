using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic attack range upgrade  _", menuName = "Upgrade/Basic attack/Basic attack range")]
public class BasicAttackRangeUpgrade : Upgrade
{
    public float bonusRange;

    public override void Apply()
    {
        Transform basicAttackHitBox = playerCombatController.BasicAttackHitBox.transform;
        basicAttackHitBox.position += basicAttackHitBox.forward * bonusRange;
        playerCombatController.BasicAttackHitBox.GetComponent<SphereCollider>().radius += bonusRange;
        Transform[] particlesTransforms = playerCombatController.BasicAttackHitBox.transform.GetComponentsInChildren<Transform>();
        foreach (Transform particleTransform in particlesTransforms)
        {
            particleTransform.localScale += Vector3.one * bonusRange;
        }
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent, false);
    }
}
