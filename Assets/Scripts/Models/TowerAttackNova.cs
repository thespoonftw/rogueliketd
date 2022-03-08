using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackNova : TowerAttack {

    public TowerAttackNova(Tower tower) : base(tower) {

    }

    public override bool TryAttack() {
        var enemiesWithinRange = GetEnemiesWithinRange();
        if (enemiesWithinRange.Count == 0) { return false; }
        enemiesWithinRange.ForEach(e => AttackEnemy(e));
        return true;
    }
    
}
