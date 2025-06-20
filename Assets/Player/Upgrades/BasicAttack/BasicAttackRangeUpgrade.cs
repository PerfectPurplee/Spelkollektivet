using System.Collections.Generic;
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

    public override List<StatChange> GetStatsChanges()
    {
        string previousRange = playerCombatController.BasicAttackHitBox.transform.localScale.x.ToString("0.00");
        string rangeAfterUpgrade = (playerCombatController.BasicAttackHitBox.transform.localScale.x + bonusRange).ToString("0.00");
        return new List<StatChange> {
            new StatChange(
                $"Basic Attack Range: {previousRange} => {rangeAfterUpgrade}",
                Positivity.Positive) 
        };
    }
}
