using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttack {

    protected DataStructure data;
    protected Structure structure;
    protected Tower tower;
    protected WaveManager waveManager;
    protected Enemy currentTarget;

    public TowerAttack(Tower tower) {
        waveManager = WaveManager.Instance;
        this.tower = tower;
        structure = tower.structure;
        data = structure.data;
    }

    public virtual bool TryAttack() {
        if (!IsCurrentTargetWithinRange()) {
            var enemiesWithinRange = GetEnemiesWithinRange();
            if (enemiesWithinRange.Count == 0) { return false; }
            currentTarget = enemiesWithinRange[0];
        }
        if (currentTarget == null) { return false; }
        AttackEnemy(currentTarget);
        return true;
    }

    protected bool IsCurrentTargetWithinRange() {
        if (currentTarget == null) { return false; }
        if (!currentTarget.IsAlive) { return false; }
        return IsEnemyWithinRange(currentTarget);
    }

    protected List<Enemy> GetEnemiesWithinRange() {
        var enemies = waveManager.GetLivingEnemies();
        return enemies.Where(e => IsEnemyWithinRange(e)).ToList();
    }

    protected bool IsEnemyWithinRange(Enemy enemy) {
        return Vector3.Distance(structure.position, enemy.CurrentPosition) < data.range;
    }

    protected void AttackEnemy(Enemy enemy) {
        ProjectileManager.Instance.ShootProjectile(tower, enemy);
    }

}
