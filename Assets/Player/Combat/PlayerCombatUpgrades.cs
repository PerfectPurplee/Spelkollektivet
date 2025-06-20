using System;
using Enemies;
using UnityEngine;

public partial class PlayerCombatController
{
    public SingleAttackHitBox BasicAttackHitBox => basicAttackHitBox;
    public Cooldown DashCooldown => dashCooldown;

    public event Action OnShieldCatchWhileDashing;
    public SlampCast SlamCast => slamCast;

    public SingleAttackHitBox ThrowAttackHitBox => throwAttackHitBox;

    public float ThrowReturnSpeed
    {
        get { return shieldThrowingForwardDuration; }
        set { shieldThrowingForwardDuration = value; }
    }
}
