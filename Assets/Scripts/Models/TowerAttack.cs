using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TowerAttack {

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

    public abstract void TryAttack();

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
