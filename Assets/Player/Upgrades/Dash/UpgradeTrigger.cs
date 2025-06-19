using System;
using UnityEngine;

public abstract class UpgradeTrigger : ScriptableObject
{
    public Action OnTrigger;

    public abstract void Apply();

    protected Player.Player player => Player.Player.Instance;
    protected PlayerCombatController playerCombat => PlayerCombatController.Instance;
}
