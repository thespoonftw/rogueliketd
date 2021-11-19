using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellerView : MonoBehaviour {

    private Traveller model;
    private float speed = 3;
    private float timeStep;
    private float timeSinceStep;
    private GameManager game;

    public void Init() {
        game = GameManager.Instance;
        timeStep = 1 / speed;
        var centre = (Constants.GAME_GRID_SIZE - 1) / 2;
        model = new Traveller(GameManager.Instance.GameGrid, GameManager.Instance.GameGrid.GetBlock(centre, centre));
        UpdatePosition();
    }

    private void Update() {
        timeSinceStep += Time.deltaTime;
        if (timeSinceStep > timeStep) {
            timeSinceStep -= timeStep;
            model.MoveToNextTile();
            UpdateRotation();
        }
        UpdatePosition();
    }

    private void UpdateRotation() {
        if (model.NextTile == null) { return; }
        var prev = game.GameGridView.GetTilePosition(model.PrevTile);
        var next = game.GameGridView.GetTilePosition(model.NextTile);
        //transform.rotation = Quaternion.LookRotation(next - prev, Vector3.back);
    }

    private void UpdatePosition() {
        if (model.NextTile == null) {
            GameManager.Instance.FinishTravellerPath();
            Destroy(gameObject);            
        } else {
            var lambda = timeSinceStep / timeStep;
            var prev = game.GameGridView.GetTilePosition(model.PrevTile);
            var next = game.GameGridView.GetTilePosition(model.NextTile);
            transform.position = Vector3.Lerp(prev, next, lambda);
        }
    }
    
}
