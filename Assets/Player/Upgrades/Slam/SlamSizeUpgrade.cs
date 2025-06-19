using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slam size upgrade  _", menuName = "Upgrade/Slam/Slam size")]
public class SlamSizeUpgrade : Upgrade
{
    [SerializeField]
    private float bonusSize = 0.5f;

    public override void Apply()
    {
        playerCombatController.SlamCast.size += bonusSize;
    }

    public override List<StatChange> GetStatsChanges()
    {
        float sizeBefore = playerCombatController.SlamCast.size;
        float sizeAfter = sizeBefore + bonusSize;
        return new List<StatChange>
        {
            new StatChange
            {
                text = $"{SlamText()} Size: {sizeBefore} => {sizeAfter}",
                positivity = Positivity.Positive
            }
        };
    }
}
