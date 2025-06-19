using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empowered attack x decrease upgrade", menuName = "Upgrade/Empowered attack/x decrease")]
public class EmpoweredAttackXDecraeseUpgrade : Upgrade
{
    public override void Apply()
    {
        playerCombatController.empoweredAttackEveryX--;
    }

    public override List<StatChange> GetStatsChanges()
    {
        int previousX = playerCombatController.empoweredAttackEveryX;
        int nextX = previousX - 1;
        return new List<StatChange> {
            new StatChange(
                $"Empowered Attack every {previousX} attacks => every {nextX} attacks",
                Positivity.Positive) };
    }
}
