using UnityEngine;
using System.Collections.Generic;

public abstract class Upgrade : ScriptableObject
{
    public int maxStacks = 100;
    public GameObject iconPrefab;
    public List<Upgrade> conflictingUpgrades;
    public List<Upgrade> requiredUpgrades;

    public abstract void Apply();
    public abstract GameObject InstantiateIcon(Transform parent);

    protected Player.Player player => Player.Player.Instance;
    protected PlayerCombatController playerCombatController => PlayerCombatController.Instance;
}
