using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Empowered attack enable upgrade", menuName = "Upgrade/Empowered attack/enable")]
public class EmpoweredAttackEveryXUpgrade : Upgrade
{
    public int startX = 5;

    public override void Apply()
    {
        playerCombatController.empoweredAttacks = true;
        playerCombatController.empoweredAttackEveryX = startX;
    }
}
