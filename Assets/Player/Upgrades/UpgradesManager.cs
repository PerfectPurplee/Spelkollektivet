using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public event Action<List<GameObject>> OnStartUpgradeChoice;
    public event Action<Upgrade> OnUpgradeChoice;
    public void ChooseUpgradeByIndex(int buttonIndex)
    {
        ChooseUpgrade(upgradesToChoose[buttonIndex]);
    }

    [SerializeField]
    private List<Upgrade> allUpgrades;
    [SerializeField]
    public List<Transform> iconsParents;

    private int upgradesToChooseAmount = 3;

    private List<Upgrade> upgradesAcquired = new List<Upgrade>();
    private Dictionary<Upgrade, int> upgradesAcquiredStacks = new Dictionary<Upgrade, int>();
    private List<Upgrade> upgradesPool;
    private List<Upgrade> upgradesWaitingForRequirenment;

    private List<Upgrade> upgradesToChoose;
    private List<GameObject> upgradesToChooseIcons;

    private void Awake()
    {
        upgradesPool = allUpgrades.Where(upgrade => upgrade.requiredUpgrades.Count == 0).ToList();
        upgradesWaitingForRequirenment = allUpgrades.Where(upgrade => upgrade.requiredUpgrades.Count > 0).ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartUpgradeChoice();
            //GenerateUpgradesToChoose();
            foreach (Upgrade upgradeToChoose in upgradesToChoose)
            {
                Debug.Log(upgradeToChoose.name);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChooseUpgradeByIndex(0);
            Debug.Log($"chose {upgradesToChoose[0]}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChooseUpgradeByIndex(1);
            Debug.Log($"chose {upgradesToChoose[1]}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChooseUpgradeByIndex(2);
            Debug.Log($"chose {upgradesToChoose[2]}");
        }
    }

    public void StartUpgradeChoice()
    {
        GenerateUpgradesToChoose();
        GenerateIconGameObjects();
        Time.timeScale = 0;
        OnStartUpgradeChoice?.Invoke(upgradesToChooseIcons);
    }

    private List<Upgrade> GenerateUpgradesToChoose()
    {
        upgradesToChoose = new List<Upgrade>(upgradesToChooseAmount);
        List<Upgrade> tmpPool = new List<Upgrade>(upgradesPool);
        for (int i = 0; i < upgradesToChooseAmount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, tmpPool.Count);
            upgradesToChoose.Add(tmpPool[randomIndex]);
            tmpPool.RemoveAt(randomIndex);
        }
        return upgradesToChoose;
    }

    private List<GameObject> GenerateIconGameObjects()
    {
        int i = 0;
        upgradesToChooseIcons = new List<GameObject>(upgradesToChooseAmount);
        foreach (Upgrade upgrade in upgradesToChoose)
        {
            upgradesToChooseIcons.Add(upgrade.InstantiateIcon(iconsParents[i]));
            i++;
        }
        return upgradesToChooseIcons;
    }

    public void ChooseUpgrade(Upgrade chosenUpgrade)
    {
        chosenUpgrade.Apply();

        upgradesPool = upgradesPool.Where(
            upgrade => !ConflictCheck(upgrade, chosenUpgrade)
            ).ToList();

        upgradesWaitingForRequirenment = upgradesWaitingForRequirenment.Where(
            upgrade => !ConflictCheck(upgrade, chosenUpgrade)
            ).ToList();

        if (!upgradesAcquired.Contains(chosenUpgrade))
        {
            upgradesAcquired.Add(chosenUpgrade); 
        }
        if (upgradesAcquiredStacks.ContainsKey(chosenUpgrade))
        {
            upgradesAcquiredStacks[chosenUpgrade]++;
        }
        else
        {
            upgradesAcquiredStacks[chosenUpgrade] = 1;
        }

        if (upgradesAcquiredStacks[chosenUpgrade] >= chosenUpgrade.maxStacks)
        {
            upgradesPool.Remove(chosenUpgrade);
        }

        var upgradesThatNowMeetRequirenmentd = upgradesWaitingForRequirenment.Where(
            upgradeToCheck => upgradeToCheck.requiredUpgrades.Any(requiredUpgrade => upgradesAcquired.Contains(requiredUpgrade))
            ).ToList();
        upgradesWaitingForRequirenment.RemoveAll(upgrade => upgradesThatNowMeetRequirenmentd.Contains(upgrade));
        upgradesPool.AddRange(upgradesThatNowMeetRequirenmentd);

        OnUpgradeChoice?.Invoke(chosenUpgrade);
        Time.timeScale = 1;
    }

    private bool ConflictCheck(Upgrade upgrade1, Upgrade upgrade2)
    {
        return upgrade1.conflictingUpgrades.Contains(upgrade2) || upgrade2.conflictingUpgrades.Contains(upgrade1);
    }
}

