using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empowered attack enable upgrade", menuName = "Upgrade/Empowered attack/enable")]
public class EmpoweredAttackEveryXUpgrade : Upgrade
{
    public int startX = 5;

    public override void Apply()
    {
        playerCombatController.empoweredAttacks = true;
        playerCombatController.empoweredAttackEveryX = startX;
    }

    public override List<StatChange> GetStatsChanges()
    {
        int basicDamage = playerCombatController.BasicAttackHitBox.damage;
        int empoweredDamage = Mathf.FloorToInt(playerCombatController.BasicAttackHitBox.damage * playerCombatController.empoweredAttackDamageMultiplier);
        return new List<StatChange>()
        {
            new StatChange(
                $"Basic Attack Damage: {basicDamage}\nEmpowered Attack Damage: {empoweredDamage}",
                Positivity.Positive)
        };
    }
}
