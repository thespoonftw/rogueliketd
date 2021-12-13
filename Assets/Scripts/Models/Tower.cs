using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower {

    public readonly Structure structure;
    public DataStructure data => structure.data;

    public int Damage { get; private set; }

    private Enemy currentTarget;
    private int range;
    private float attackRate;
    private float oneOverAttackRate;
    private float timeSinceLastAttack;
    private WaveManager waveManager;

    public Tower(Structure structure) {
        this.structure = structure;
        attackRate = data.rate;
        oneOverAttackRate = 1f / attackRate;
        Damage = 1; // placeholder
        StructureUpdateManager.Instance.AddTower(this);
        waveManager = WaveManager.Instance;
    }

    public void Tick(float deltaTime) {
        timeSinceLastAttack += deltaTime;
        if (timeSinceLastAttack > oneOverAttackRate) { return; }
        timeSinceLastAttack -= oneOverAttackRate;

        if (!IsTargetValid()) {
            var enemiesWithinRange = GetEnemiesWithinRange();
            if (enemiesWithinRange.Count == 0) { return; }
            currentTarget = enemiesWithinRange[0];
        }
        if (currentTarget == null) { return; }

        AttackEnemy(currentTarget);
    }

    private bool IsTargetValid() {
        if (currentTarget == null) { return false; }
        if (!currentTarget.IsAlive) { return false; }
        return Vector3.Distance(structure.position, currentTarget.CurrentPosition) < data.range;
    }

    private List<Enemy> GetEnemiesWithinRange() {
        var enemies = waveManager.GetLivingEnemies();
        return enemies.Where(e => Vector3.Distance(structure.position, e.CurrentPosition) < data.range).ToList();
    }

    private void AttackEnemy(Enemy enemy) {
        ProjectileManager.Instance.ShootProjectile(this, enemy);
    }
    
}
