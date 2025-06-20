using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Throw Return Speed Upgrade", menuName = "Upgrade/Throw/Return Speed")]
public class ThrowReturnSpeedUpgrade : Upgrade
{
    public float timeDecrease = 0.1f;

    public override void Apply()
    {
        playerCombatController.ThrowReturnSpeed -= timeDecrease;
    }

    public override List<StatChange> GetStatsChanges()
    {
        return new List<StatChange>
        {
            new StatChange
            {
                text = $"Return speed: {playerCombatController.ThrowReturnSpeed} => {playerCombatController.ThrowReturnSpeed - timeDecrease}",
                positivity = Positivity.Positive
            }
        };
    }
}
