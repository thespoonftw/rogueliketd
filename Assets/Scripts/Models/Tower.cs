using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower {

    public readonly Structure structure;
    public DataStructure data => structure.data;

    public int Damage { get; private set; }

    private float attackRate;
    private float oneOverAttackRate;
    private float timeSinceLastAttack;

    private TowerAttack towerAttack;

    public Tower(Structure structure) {
        this.structure = structure;
        attackRate = data.rate;
        oneOverAttackRate = 1f / attackRate;
        Damage = data.damage; // placeholder
        StructureUpdateManager.Instance.AddTower(this);
        

        switch (structure.data.action) {
            case StructureAction.nova: towerAttack = new TowerAttackNova(this); break;
            case StructureAction.bomb: towerAttack = new TowerAttackArc(this); break;
            case StructureAction.boulder: towerAttack = new TowerAttackTrap(this, 1, 3, 1); break;
            default: towerAttack = new TowerAttack(this); break;
        }
    }

    public void Tick(float deltaTime) {
        timeSinceLastAttack += deltaTime;
        if (timeSinceLastAttack < oneOverAttackRate) { return; }
        timeSinceLastAttack -= oneOverAttackRate;
        towerAttack.TryAttack();
    }
    
}
