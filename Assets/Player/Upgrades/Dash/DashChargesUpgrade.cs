using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash charge upgrade  _", menuName = "Upgrade/Dash/Dash charge")]
public class DashChargesUpgrade : Upgrade
{
    public override void Apply()
    {
        playerCombatController.DashCooldown.maxCharges++;
    }

    public override List<StatChange> GetStatsChanges()
    {
        int currentDashCharges = playerCombatController.DashCooldown.maxCharges;
        return new List<StatChange>
        {
            new StatChange(
                $"{DashText()} charges: {currentDashCharges} => {currentDashCharges + 1}",
                Positivity.Positive
                )
        };
    }
}
