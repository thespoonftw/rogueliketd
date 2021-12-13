using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileView : MonoBehaviour {

    private Projectile model;

    public void Init(Projectile model) {
        this.model = model;
        model.OnDeath += Destroy;
        model.OnPosition += UpdatePosition;
    }

    private void Destroy() {
        Destroy(gameObject);
    }

    private void UpdatePosition() {
        transform.position = model.CurrentPosition;
    }
}
