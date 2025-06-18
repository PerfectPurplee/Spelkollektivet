using System;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public int maxCharges = 1;
    public int charges = 1;
    public float duration;
    public float lastUseTime;

    public float CurrentProgress => Time.time - lastUseTime;

    public bool IsUsable => charges > 0;

    public event Action<int> OnRecharge;

    private void Update()
    {
        if (Time.time - lastUseTime > duration && charges < maxCharges)
        {
            charges++;
            lastUseTime = Time.time;
            OnRecharge?.Invoke(charges);
        }
    }

    public void Use()
    {
        charges -= 1;
        lastUseTime = Time.time;
    }
}
