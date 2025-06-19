using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empowered attack damage upgrade", menuName = "Upgrade/Empowered attack/damage multiplier")]
public class EmpoweredAttackDamageMultiplierUpgrade : Upgrade
{
    public float bonusMultiplier = 0.2f;

    public override void Apply()
    {
        playerCombatController.empoweredAttackDamageMultiplier += bonusMultiplier;
    }

    public override List<StatChange> GetStatsChanges()
    {
        int baseDamage = playerCombatController.BasicAttackHitBox.damage;
        int currentDamage = Mathf.FloorToInt(baseDamage * playerCombatController.empoweredAttackDamageMultiplier);
        int damageAfterUpgrade = Mathf.FloorToInt(baseDamage * (playerCombatController.empoweredAttackDamageMultiplier + bonusMultiplier));
        return new List<StatChange> {
            new StatChange(
                $"Empowered Attack Damage: {currentDamage} => {damageAfterUpgrade}",
                Positivity.Positive)
        };
    }
}
