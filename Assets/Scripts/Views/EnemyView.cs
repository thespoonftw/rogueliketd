using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour {

    [SerializeField] GameObject healthBarParent;
    [SerializeField] GameObject healthBarRed;
    [SerializeField] GameObject healthBarBlack;

    private Enemy model;

    public void Init(Enemy model) {
        Debug.Log("init");
        this.model = model;
        model.OnDeath += Destroy;
        model.OnPosition += UpdatePosition;
        model.OnHealthChange += UpdateHealth;
        model.OnTileChange += UpdateTile;
        UpdateHealth();
    }

    private void Destroy() {
        Destroy(gameObject);
    }

    private void UpdatePosition() {
        transform.position = model.CurrentPosition;
    }

    private void UpdateTile() {
        if (model.NextTile != null) {
            var prev = model.PrevTile.position;
            var next = model.NextTile.position;
            transform.rotation = Quaternion.LookRotation(next - prev, Vector3.up);
        }
        healthBarParent.transform.rotation = Quaternion.identity;
    }

    private void OnDestroy() {
        model.OnHealthChange -= UpdateHealth;
        model.OnDeath -= Destroy;
        model.OnPosition -= UpdatePosition;
    }

    private void Update() {
        model.Tick(Time.deltaTime);
    }    
    
    private void UpdateHealth() {
        var f = model.CurrentHealth / model.maxHealth;
        healthBarRed.transform.localScale = new Vector3(f, 1, 1);
        healthBarBlack.transform.localScale = new Vector3(1 - f, 1, 1);
    }
}
