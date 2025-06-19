using TMPro;
using UnityEngine;

public class UpgradeStatField : MonoBehaviour
{
    [Header("Prefab Refrences")]
    [SerializeField]
    private TextMeshProUGUI TMP;
    [SerializeField]
    private Color positiveColor;
    [SerializeField]
    private Color negativeColor;
    [SerializeField]
    private Color neutralColor;

    [Header("Variables")]
    public Positivity positivity;
    public string text;

    private bool alreadyApplied = false;

    private void Start()
    {
        if (!alreadyApplied)
        {
            ApplyData();
        }
    }

    public void ApplyData()
    {
        alreadyApplied = true;

        switch (positivity)
        {
            case Positivity.Positive:
                TMP.color = positiveColor;
                break;
            case Positivity.Negative:
                TMP.color = negativeColor;
                break;
            case Positivity.Neutral:
                TMP.color = neutralColor;
                break;
        }
        TMP.text = text;
    }
}

public enum Positivity
{
    None,
    Positive,
    Negative,
    Neutral
}