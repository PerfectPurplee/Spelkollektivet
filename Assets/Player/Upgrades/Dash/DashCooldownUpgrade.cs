using UnityEngine;

[CreateAssetMenu(fileName = "Dash Cooldown upgrade  _", menuName = "Upgrade/Dash/Dash cooldown")]
public class DashCooldownUpgrade : Upgrade
{
    public override void Apply()
    {
        playerCombatController.DashCooldown.duration *= 0.9f;
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent);
    }
}
