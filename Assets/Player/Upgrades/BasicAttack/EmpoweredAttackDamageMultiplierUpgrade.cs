using UnityEngine;

[CreateAssetMenu(fileName = "Empowered attack damage upgrade", menuName = "Upgrade/Empowered attack/damage multiplier")]
public class EmpoweredAttackDamageMultiplierUpgrade : Upgrade
{
    public float bonusMultiplier = 0.2f;

    public override void Apply()
    {
        playerCombatController.empoweredAttackDamageMultiplier += bonusMultiplier;
    }
}
