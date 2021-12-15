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

    public override void TryAttack() {
        
    }

    private bool IsEnemyWithinSquare(Enemy enemy) {

        return true;
    }

}
