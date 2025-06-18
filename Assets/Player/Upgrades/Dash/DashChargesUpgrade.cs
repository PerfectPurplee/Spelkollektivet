using UnityEngine;

[CreateAssetMenu(fileName = "Dash charge upgrade  _", menuName = "Upgrade/Dash/Dash charge")]
public class DashChargesUpgrade : Upgrade
{
    public override void Apply()
    {
        playerCombatController.DashCooldown.maxCharges++;
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent);
    }
}
