using UnityEngine;

[CreateAssetMenu(fileName = "Slam damage upgrade _", menuName = "Upgrade/Slam/Slam damage")]
public class SlamDamageUpgrade : Upgrade
{
    public int bonusDamage;

    public override void Apply()
    {
        playerCombatController.SlamCast.damage += bonusDamage;
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent);
    }
}
