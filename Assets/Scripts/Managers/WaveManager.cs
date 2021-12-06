using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    

    [SerializeField] GameObject travellerPrefab;

    public event Action<bool> OnRoundInProgress;

    private float timeTilNextSpawn;
    private int livingEnemies = 0;
    private bool spawningInProgress;
    private int pointsPerWave = 10;
    private int waveSizeIndex = 5;
    private const int waveDuration = 20;
    private float timeInWave;
    private int pointsPerSecond;

    public void StartWave() {
        CanvasManager.Instance.SetRoundInProgress(true);
        OnRoundInProgress?.Invoke(true);
        spawningInProgress = true;
        timeTilNextSpawn = 0;
        pointsPerWave += 10;
        timeInWave = 0;
        pointsPerSecond = pointsPerWave / waveDuration;
    }

    private void Update() {
        if (!spawningInProgress) { return; }
        timeTilNextSpawn -= Time.deltaTime;
        if (timeTilNextSpawn > 0) { return; }
        var enemySize = Data.EnemySpawnChances.GetEntry(waveSizeIndex).RandomlySelectSize();
        var enemyStats = Data.EnemyStats.GetEntry(enemySize);
        timeTilNextSpawn += (enemyStats.points / pointsPerSecond);
        var go = Instantiate(travellerPrefab, new Vector3(), Quaternion.identity, transform);
        go.transform.localScale = new Vector3(enemyStats.scale, enemyStats.scale, enemyStats.scale);
        go.GetComponent<TravellerView>().Init();
        livingEnemies++;
        timeInWave += Time.deltaTime;
        if (timeInWave >= waveDuration) { spawningInProgress = false; }
    }

    public void FinishTravellerPath() {
        livingEnemies--;
        if (livingEnemies == 0 && !spawningInProgress) {
            OnRoundInProgress?.Invoke(false);
        }
        // do something?
    }

}
