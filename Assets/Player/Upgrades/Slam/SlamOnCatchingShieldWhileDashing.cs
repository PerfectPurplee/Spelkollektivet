using UnityEngine;

[CreateAssetMenu(fileName = "Slam on catching shiled while dashing _", menuName = "Upgrade/Slam/Slam on catching shield while dashing")]
public class SlamOnCatchingShieldWhileDashing : Upgrade
{
    public override void Apply()
    {
        playerCombatController.OnShieldCatchWhileDashing += playerCombatController.SlamCast.CastOnPlayer;
    }
}
