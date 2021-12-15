using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttackArc : TowerAttack {

    public TowerAttackArc(Tower tower) : base(tower) {
          
    }

    public override void TryAttack() {
        if (!IsCurrentTargetWithinArc()) {
            var enemiesWithinRange = GetEnemiesWithinArc();
            if (enemiesWithinRange.Count == 0) { return; }
            currentTarget = enemiesWithinRange[0];
        }
        if (currentTarget == null) { return; }

        AttackEnemy(currentTarget);
    }

    private bool IsCurrentTargetWithinArc() {
        if (currentTarget == null) { return false; }
        if (!currentTarget.IsAlive) { return false; }
        return (IsEnemyWithinRange(currentTarget) && IsEnemyWithinArc(currentTarget));
    }

    private List<Enemy> GetEnemiesWithinArc() {
        var enemies = waveManager.GetLivingEnemies();
        return enemies.Where(e => IsEnemyWithinRange(e) && IsEnemyWithinArc(e)).ToList();
    }

    private bool IsEnemyWithinArc(Enemy enemy) {
        var vectorToEnemy = enemy.CurrentPosition - structure.position;
        var forward = structure.direction.GetVectorAfterRotation(Vector3.back);
        var angle = Vector3.SignedAngle(vectorToEnemy, forward, Vector3.up);
        return (Mathf.Abs(angle) < Constants.BOMB_ARC / 2f);
    }

}
