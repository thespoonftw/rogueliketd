using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackStandard : TowerAttack {

    public TowerAttackStandard(Tower tower) : base(tower) {
          
    }

    public override void TryAttack() {
        if (!IsCurrentTargetWithinRange()) {
            var enemiesWithinRange = GetEnemiesWithinRange();
            if (enemiesWithinRange.Count == 0) { return; }
            currentTarget = enemiesWithinRange[0];
        }
        if (currentTarget == null) { return; }

        AttackEnemy(currentTarget);
    }

    private bool IsCurrentTargetWithinRange() {
        if (currentTarget == null) { return false; }
        if (!currentTarget.IsAlive) { return false; }
        return IsEnemyWithinRange(currentTarget);
    }

}
