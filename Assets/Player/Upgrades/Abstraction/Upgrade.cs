using UnityEngine;
using System.Collections.Generic;

public abstract class Upgrade : ScriptableObject
{
    public string nameText;
    public Sprite sprite;
    public int maxStacks = 100;
    public UpgradeChoiceField choiceFieldPrefab;
    public List<Upgrade> conflictingUpgrades;
    public List<Upgrade> requiredUpgrades;

    public abstract void Apply();
    public abstract List<StatChange> GetStatsChanges();

    public virtual GameObject InstantiateChoiceField(Transform parent)
    {
        if (choiceFieldPrefab == null)
        {
            Debug.Log(name);
        }
        UpgradeChoiceField choiceFieldInstance = Instantiate(choiceFieldPrefab, parent);
        choiceFieldInstance.ApplyData(sprite, nameText, GetStatsChanges());
        return choiceFieldInstance.gameObject;
    }

    protected Player.Player player => Player.Player.Instance;
    protected PlayerCombatController playerCombatController => PlayerCombatController.Instance;

    protected bool CheckIfInt(float value)
    {
        return Mathf.Approximately(Mathf.RoundToInt(value), value);
    }
    protected string EmpoweredAttackText() => "<color=#ed6339>Empowered Attack</color>";
    protected string DashText() => "<color=#7af0d8>Dash</color>";
    protected string SlamText() => "<color=#e8f07a>Slam</color>";
    protected string CatchText() => "<color=#927af0>Catch</color>";
}
