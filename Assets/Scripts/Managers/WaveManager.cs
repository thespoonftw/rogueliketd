using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] GameObject travellerPrefab;

    public event Action<bool> OnRoundInProgress;

    public float SpeedModifier { get; private set; } = 0.1f;

    private float timeTilNextSpawn;
    private bool spawningInProgress;
    private int pointsPerWave = 10;
    private int waveSizeIndex = 5;
    private const int waveDuration = 20;
    private float timeInWave;
    private int pointsPerSecond;
    private List<Enemy> livingEnemies = new List<Enemy>();
    

    private DataEnemyStat upcomingEnemy;

    public void StartWave() {
        CanvasManager.Instance.SetRoundInProgress(true);
        OnRoundInProgress?.Invoke(true);
        spawningInProgress = true;
        timeTilNextSpawn = 0;
        pointsPerWave += 10;
        timeInWave = 0;
        pointsPerSecond = pointsPerWave / waveDuration;
        upcomingEnemy = GetNextEnemy();
    }

    private void Update() {
        if (!spawningInProgress) { return; }
        timeInWave += Time.deltaTime;
        timeTilNextSpawn -= Time.deltaTime;
        if (timeInWave >= waveDuration) {
            spawningInProgress = false;
        } else if (timeTilNextSpawn <= 0) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        var enemyToSpawn = upcomingEnemy;
        upcomingEnemy = GetNextEnemy();
        timeTilNextSpawn += ((enemyToSpawn.points + upcomingEnemy.points) / (float)pointsPerSecond) / 2f;
        var go = Instantiate(travellerPrefab, new Vector3(), Quaternion.identity, transform);
        var scale = enemyToSpawn.scale;
        go.transform.localScale = new Vector3(scale, scale, scale);
        
        var enemy = new Enemy(GameManager.Instance.GameGrid, GameManager.Instance.GameGrid.GetStartBlock(), enemyToSpawn);
        go.GetComponent<EnemyView>().Init(enemy);
        livingEnemies.Add(enemy);
    }

    private DataEnemyStat GetNextEnemy() {
        var enemySize = Data.EnemySpawnChances.GetEntry(waveSizeIndex).RandomlySelectSize();
        return Data.EnemyStats.GetEntry(enemySize);
    }

    public void FinishTravellerPath(Enemy enemy) {
        GameManager.Instance.ModifyLives(-1);
        RemoveEnemy(enemy);
    }

    public void RemoveEnemy(Enemy enemy) {
        livingEnemies.Remove(enemy);
        if (livingEnemies.Count == 0 && !spawningInProgress) {
            OnRoundInProgress?.Invoke(false);
        }
    }

    public List<Enemy> GetLivingEnemies() {
        return new List<Enemy>(livingEnemies);
    }

}
