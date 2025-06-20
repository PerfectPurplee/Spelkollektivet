using System.Collections.Generic;
using Boss;
using Enemies;
using Interface;
using NUnit.Framework;
using UnityEngine;

public class BossStompAttack : BossAttack {
    public override float TriggerDistance => 7f;
    public override AttackType AttackType => AttackType.Melee;
    public override float AttackDuration => 4f;
    public override int Damage => 30;


    public override string AnimatorAttackName {
        get => "StompAttack";
        set { }
    }

    [SerializeField] protected List<BossDamageApplier> attackDamageApplierList;


    private void Start() {
        foreach (BossDamageApplier damageApplier in attackDamageApplierList) {
            damageApplier.Attack = this;
        }
    }

    public override bool TryAttack() {
        if (this.IsOnCooldown) return false;
        if (GameBoss.distanceToPlayer < this.TriggerDistance && GameBoss.BossState != BossState.Attacking) {
            InvokeOnBossAttack();
            CreateDamageAppliers();
            LastAttackTime = Time.time;
            return true;
        }

        return false;
    }

    private void CreateDamageAppliers() {
        var instance = Instantiate(attackDamageApplierList[0].gameObject, transform);
        if (instance.TryGetComponent<IDamageApplier>(out var applier)) {
            applier.Attack = this;
        }
    }
}