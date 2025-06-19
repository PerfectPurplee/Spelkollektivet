using UnityEngine;

[CreateAssetMenu(fileName = "_ on _ upgrade", menuName = "Upgrade/Action on trigger")]
public class UpgradeActionOnTrigger : Upgrade
{
    public UpgradeAction action;
    public UpgradeTrigger trigger;

    public override void Apply()
    {
        trigger.Subscribe(action.GetAction());
    }
}
