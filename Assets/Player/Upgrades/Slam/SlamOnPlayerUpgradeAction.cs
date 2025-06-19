using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Slam On Player Action", menuName = "Upgrade/Slam/Slam on player action", order = 1000)]
public class SlamOnPlayerUpgradeAction : UpgradeAction
{
    public override Action GetAction()
    {
        return playerCombat.SlamCast.CastOnPlayer;
    }
}
