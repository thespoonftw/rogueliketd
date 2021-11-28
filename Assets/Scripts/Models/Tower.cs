using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower {

    private Structure structure;
    private bool isRangedAttacker;
    private Enemy lastTarget;
    private int range;
    private float attackRate;

    public Tower(Structure structure) {
        this.structure = structure;
        //isRangedAttacker = (structure.data.type == StructureType.towerMelee);
        StructureUpdateManager.Instance.AddTower(this);
    }

    public void Tick(float deltaTime) {

    }

    private List<Enemy> GetEnemies() {
        return isRangedAttacker ? GetEnemiesRanged() : GetEnemiesMelee();
    }

    private List<Enemy> GetEnemiesMelee() {
        return structure.GetAreaOfEffect().SelectMany(t => t.GetEnemies()).Distinct().ToList();
    }

    private List<Enemy> GetEnemiesRanged() {
        return new List<Enemy>();
    }
    
}
