using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Catch while dash trigger", menuName = "Upgrade/Dash/Catch while dash trigger", order = 1000)]
public class CatchWhileDashUpgradeTrigger : UpgradeTrigger
{
    public override void Subscribe(Action action)
    {
        playerCombat.OnShieldCatchWhileDashing += action;
    }   
}
