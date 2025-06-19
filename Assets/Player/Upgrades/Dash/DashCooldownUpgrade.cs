using UnityEngine;

[CreateAssetMenu(fileName = "Dash Cooldown upgrade  _", menuName = "Upgrade/Dash/Dash cooldown")]
public class DashCooldownUpgrade : Upgrade
{
    public float multiplier = 0.9f;

    public override void Apply()
    {
        playerCombatController.DashCooldown.duration *= multiplier;
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent);
    }
}
