using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    private Grid grid;
    public Tile PrevTile { get; private set; }
    public Tile NextTile { get; private set; }
    public float CurrentHealth { get; private set; }
    public Vector3 CurrentPosition { get; private set; }

    public readonly float speed;
    public readonly float maxHealth;

    private readonly float oneOverSpeed;

    private float timeSinceStep;

    public event Action OnHealthChange;
    public event Action OnDeath;
    public event Action OnPosition;
    public event Action OnTileChange;

    private GameManager game;

    public Enemy(Grid grid, Block startBlock, DataEnemyStat enemy) {
        game = GameManager.Instance;
        this.grid = grid;
        NextTile = startBlock.GetStartPath();
        MoveToNextTile();
        speed = enemy.speed * WaveManager.Instance.SpeedModifier;
        oneOverSpeed = 1f / speed;
        maxHealth = enemy.health;
        CurrentHealth = maxHealth;
    }

    public void Tick(float deltaTime) {
        timeSinceStep += deltaTime;
        if (timeSinceStep > oneOverSpeed) {
            timeSinceStep -= oneOverSpeed;
            MoveToNextTile();
        }
        UpdatePosition();
    }

    private void UpdatePosition() {
        if (NextTile == null) {
            WaveManager.Instance.FinishTravellerPath(this);
            OnDeath.Invoke();            
        } else {
            var lambda = timeSinceStep * speed;
            var prev = game.GameGridView.GetTilePosition(PrevTile);
            var next = game.GameGridView.GetTilePosition(NextTile);
            CurrentPosition = Vector3.Lerp(prev, next, lambda);
            OnPosition.Invoke();
        }
    }


    public void MoveToNextTile() {
        var prev = PrevTile;
        PrevTile = NextTile;
        NextTile = NextTile.GetNextPath(prev);
        OnTileChange.Invoke();
    }
    
}
