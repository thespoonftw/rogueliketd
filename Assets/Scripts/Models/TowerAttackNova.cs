using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackNova : TowerAttack {

    public TowerAttackNova(Tower tower) : base(tower) {

    }

    public override void TryAttack() {
        var enemiesWithinRange = GetEnemiesWithinRange();
        enemiesWithinRange.ForEach(e => AttackEnemy(e));
    }
    
}
