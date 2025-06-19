using UnityEngine;

[CreateAssetMenu(fileName = "Empowered attack x decrease upgrade", menuName = "Upgrade/Empowered attack/x decrease")]
public class EmpoweredAttackXDecraeseUpgrade : Upgrade
{
    public override void Apply()
    {
        playerCombatController.empoweredAttackEveryX--;
    }
}
