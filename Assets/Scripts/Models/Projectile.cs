using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile {

    public event Action OnDeath;
    public event Action OnPosition;

    public Vector3 CurrentPosition { get; private set; }
    public bool IsMarkedForRemoval { get; private set; }

    private Tower source;
    private Enemy target;
    private int damage;
    private float speed = 0.1f;
    private Vector3 lastEnemyPosition;

    public Projectile(Tower source, Enemy target, int damage) {
        CurrentPosition = source.structure.position;
        this.source = source;
        this.target = target;
        this.damage = damage;
    }

    public void Tick() {
        var targetPos = target != null ? target.CurrentPosition : lastEnemyPosition;
        if (target != null) { lastEnemyPosition = target.CurrentPosition; }

        lastEnemyPosition = target.CurrentPosition;
        CurrentPosition = Vector3.MoveTowards(CurrentPosition, targetPos, speed);
        if (Vector3.Distance(CurrentPosition, targetPos) < 0.1f) {
            target?.Damage(damage);
            OnDeath.Invoke();
            IsMarkedForRemoval = true;
        } else {
            OnPosition.Invoke();
        }
    }
    
}
