using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slam damage upgrade _", menuName = "Upgrade/Slam/Slam damage")]
public class SlamDamageUpgrade : Upgrade
{
    public int bonusDamage;

    public override void Apply()
    {
        playerCombatController.SlamCast.damage += bonusDamage;
    }

    public override List<StatChange> GetStatsChanges()
    {
        int currentDamage = playerCombatController.SlamCast.damage;
        int afterUpgradeDamage = currentDamage + bonusDamage;
        return new List<StatChange>
        {
            new StatChange
            {
                text = $"{SlamText()} Damage: {currentDamage} => {afterUpgradeDamage}",
                positivity = Positivity.Positive
            }
        };
    }
}
