using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Empowered attack upgrade trigger", menuName = "Upgrade/Empowered attack/trigger", order = 100)]
public class EmpoweredAttackUpgradeTrigger : UpgradeTrigger
{
    public override void Subscribe(Action action)
    {
        playerCombat.OnEmpoweredAttack += action;
    }
}
