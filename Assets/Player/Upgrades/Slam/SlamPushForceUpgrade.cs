using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slam push force upgrade _", menuName = "Upgrade/Slam/Slam push force")]
public class SlamPushForceUpgrade : Upgrade
{
    public float bonusForce;

    public override void Apply()
    {
        playerCombatController.SlamCast.pushForce += bonusForce;
    }

    public override List<StatChange> GetStatsChanges()
    {
        float pushForceBefore = playerCombatController.SlamCast.pushForce;
        float pushForceAfter = pushForceBefore + bonusForce;
        return new List<StatChange>
        {
            new StatChange
            {
                text = $"{SlamText()} pushes with force: {pushForceBefore} => {pushForceAfter}",
                positivity = Positivity.Positive
            }
        };
    }
}
