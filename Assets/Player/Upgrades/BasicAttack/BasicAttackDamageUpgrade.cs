using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Basic attack damage upgrade _", menuName = "Upgrade/Basic attack/Basic attack damage")]
public class BasicAttackDamageUpgrade : Upgrade
{
    [SerializeField]
    private int bonusDamage;

    public override void Apply()
    {
        playerCombatController.BasicAttackHitBox.damage += bonusDamage;
    }

    public override List<StatChange> GetStatsChanges()
    {
        return new List<StatChange> {
            new StatChange(
                $"Basic Attack Damage: {playerCombatController.BasicAttackHitBox.damage} => {playerCombatController.BasicAttackHitBox.damage + bonusDamage}",
                Positivity.Positive) };
    }
}
