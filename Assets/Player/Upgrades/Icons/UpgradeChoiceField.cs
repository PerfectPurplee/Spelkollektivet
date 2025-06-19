using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChoiceField : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI TMPUpgradeName;
    [SerializeField]
    private Transform statsChangesParent;
    [SerializeField]
    private UpgradeStatField upgradeStatFieldPrefab;

    public void ApplyData(Sprite iconSprite, string upgradeText, List<StatChange> statChanges)
    {
        iconImage.sprite = iconSprite;
        TMPUpgradeName.text = upgradeText;
        foreach (StatChange statChange in statChanges)
        {
            //Debug.Log("xddfgafdgssfdgsfdd");
            UpgradeStatField newField = Instantiate(upgradeStatFieldPrefab, statsChangesParent);
            Debug.Log(newField.name);
            newField.text = statChange.text;
            newField.positivity = statChange.positivity;
            newField.ApplyData();
            //UpgradeStatField newField = Instantiate(upgradeStatFieldPrefab, statsChangesParent);
            //newField.text = statChange.text;
            //newField.value = statChange.value;
            //newField.valueAfterUpgrade = statChange.valueAfterUpgrade;
            //newField.positivity = statChange.positivity;
            //newField.ApplyData();
        }
    }
}

[Serializable]
public struct StatChange
{
    public string text;
    public Positivity positivity;

    public StatChange(string text, Positivity positivity)
    {
        this.text = text;
        this.positivity = positivity;
    }
}