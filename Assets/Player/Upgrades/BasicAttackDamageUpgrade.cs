using UnityEngine;

[CreateAssetMenu(fileName = "Basic attack damage upgrade _", menuName = "Upgrade/Basic attack damage")]
public class BasicAttackDamageUpgrade : Upgrade
{
    [SerializeField]
    private int bonusDamage;

    public override void Apply()
    {
        playerCombatController.BasicAttackHitBox.damage += bonusDamage;
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent, false);
    }
}
