using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttackTrap : TowerAttack {

    private int width;
    private int height;
    private int offset;

    public TowerAttackTrap(Tower tower, int width, int height, int offset) : base(tower) {
        this.width = width;
        this.height = height;
        this.offset = offset;
    }

    public override bool TryAttack() {
        var targets = GetEnemiesWithinSquare();
        if (targets.Count == 0) { return false; }
        targets.ForEach(t => AttackEnemy(t));
        return true;
    }

    public List<Enemy> GetEnemiesWithinSquare() {
        var targetTile = tower.structure.originTile.GetAdjacentTile(tower.structure.direction);
        Debug.Log(targetTile.coords.ToString());
        return targetTile.GetEnemies();
    }
}
