using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour {

    [SerializeField] GameObject healthBarParent;
    [SerializeField] GameObject healthBarRed;
    [SerializeField] GameObject healthBarBlack;

    private Enemy model;
    private GridView gameGridView;

    public void Init(Enemy model) {
        gameGridView = GameManager.Instance.GameGridView;
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
        healthBarParent.transform.rotation = Quaternion.identity;
        if (model.NextTile == null) { return; }
        var prev = gameGridView.GetTilePosition(model.PrevTile);
        var next = gameGridView.GetTilePosition(model.NextTile);
        transform.rotation = Quaternion.LookRotation(next - prev, Vector3.up);
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
