using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Throw Damage Upgrade", menuName = "Upgrade/Throw/Damage")]
public class ThrowDamageUpgrade : Upgrade
{
    public int damageIncrease = 3;

    public override void Apply()
    {
        playerCombatController.ThrowAttackHitBox.damage += damageIncrease;
    }

    public override List<StatChange> GetStatsChanges()
    {
        float prevDamage = playerCombatController.ThrowAttackHitBox.damage;
        float afterDamage = playerCombatController.ThrowAttackHitBox.damage + damageIncrease;
        return new List<StatChange>
        {
            new StatChange
            {
                text = $"Thrown shield damage: {prevDamage} => {afterDamage}",
                positivity = Positivity.Positive
            }
        };
    }
}
