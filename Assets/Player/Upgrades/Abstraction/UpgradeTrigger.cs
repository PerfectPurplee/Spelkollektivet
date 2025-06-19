using System;
using UnityEngine;

public abstract class UpgradeTrigger : ScriptableObject
{
    public abstract void Subscribe(Action action);

    protected Player.Player player => Player.Player.Instance;
    protected PlayerCombatController playerCombat => PlayerCombatController.Instance;
}
