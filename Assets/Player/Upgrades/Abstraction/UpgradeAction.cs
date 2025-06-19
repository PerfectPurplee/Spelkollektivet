using System;
using UnityEngine;

public abstract class UpgradeAction : ScriptableObject
{
    public abstract Action GetAction();

    protected Player.Player player => Player.Player.Instance;
    protected PlayerCombatController playerCombat => PlayerCombatController.Instance;
}
