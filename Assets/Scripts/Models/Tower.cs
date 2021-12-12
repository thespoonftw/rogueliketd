using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower {

    private Structure structure;
    private Enemy currentTarget;
    private int range;
    private float attackRate;
    private float oneOverAttackRate;
    private DataStructure data;
    private float timeSinceLastAttack;

    public Tower(Structure structure, DataStructure data) {
        this.structure = structure;
        this.data = data;
        attackRate = data.rate;
        oneOverAttackRate = 1f / attackRate;
        //isRangedAttacker = (structure.data.type == StructureType.towerMelee);
        StructureUpdateManager.Instance.AddTower(this);
    }

    public void Tick(float deltaTime) {
        timeSinceLastAttack += deltaTime;
        if (timeSinceLastAttack > oneOverAttackRate) { return; }
        timeSinceLastAttack -= oneOverAttackRate;

        var enemiesWithinRange = GetEnemiesWithinRange();
        if (enemiesWithinRange.Count == 0) {
            currentTarget = null;
            return; 
        }

        if (!enemiesWithinRange.Contains(currentTarget)) {
            currentTarget = enemiesWithinRange[0];
        }

        AttackEnemy(currentTarget);
    }

    private List<Enemy> GetEnemiesWithinRange() {
        return null;
    }

    private void AttackEnemy(Enemy enemy) {
        ProjectileManager.Instance.ShootProjectile(this, enemy);
    }
    
}
