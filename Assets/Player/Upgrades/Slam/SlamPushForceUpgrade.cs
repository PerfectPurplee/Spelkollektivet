using UnityEngine;

[CreateAssetMenu(fileName = "Slam push force upgrade _", menuName = "Upgrade/Slam/Slam push force")]
public class SlamPushForceUpgrade : Upgrade
{
    public float BonusForce;

    public override void Apply()
    {
        playerCombatController.SlamCast.pushForce += BonusForce;
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent);
    }
}
