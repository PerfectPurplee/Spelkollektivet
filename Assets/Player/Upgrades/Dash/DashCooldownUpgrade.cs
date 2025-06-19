using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Cooldown upgrade  _", menuName = "Upgrade/Dash/Dash cooldown")]
public class DashCooldownUpgrade : Upgrade
{
    public float multiplier = 0.9f;

    public override void Apply()
    {
        playerCombatController.DashCooldown.duration *= multiplier;
    }

    public override List<StatChange> GetStatsChanges()
    {
        float currentDashCooldown = playerCombatController.DashCooldown.duration;
        string currentDashCooldownText = currentDashCooldown.ToString("0.00");
        string afterUpgradeDashCooldownText = (currentDashCooldown * multiplier).ToString("0.00");
        return new List<StatChange>
        {
            new StatChange
            {
                text = $"{DashText()} cooldown: {currentDashCooldownText} => {afterUpgradeDashCooldownText}",
                positivity = Positivity.Positive
            }
        };
    }
}
