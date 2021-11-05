using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellerView : MonoBehaviour {

    private Traveller model;
    private float timeBetweenUpdates = 0.3f;
    private float timeRemaining;

    public void Init() {
        model = new Traveller(GameManager.Instance.gameGrid);
        model.OnTileUpdate += UpdatePosition;
        UpdatePosition();
    }

    private void Update() {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) { 
            timeRemaining += timeBetweenUpdates;
            model.Tick();
        }
    }

    private void UpdatePosition() {
        var tile = model.CurrentTile;
        if (tile == null) {
            GameManager.Instance.FinishTravellerPath();
            Destroy(gameObject);            
        } else {
            transform.position = new Vector3(tile.X, tile.Y);
        }
    }
    
}
