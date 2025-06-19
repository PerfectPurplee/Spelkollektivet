using UnityEngine;

[CreateAssetMenu(fileName = "Slam size upgrade  _", menuName = "Upgrade/Slam/Slam size")]
public class SlamSizeUpgrade : Upgrade
{
    [SerializeField]
    private float bonusSize = 0.5f;

    public override void Apply()
    {
        playerCombatController.SlamCast.size += bonusSize;
    }

    public override GameObject InstantiateIcon(Transform parent)
    {
        return Instantiate(iconPrefab, parent);
    }
}
