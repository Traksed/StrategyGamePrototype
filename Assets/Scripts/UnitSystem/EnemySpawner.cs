using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Waves[] waves;
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;

    private void Start()
    {
        _enemiesLeftToSpawn = waves[0].WaveSettings.Length;
        LaunchWave();
    }

    private IEnumerator SpawnEnemyInWave()
    {
        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].SpawnDelay);
            Instantiate(waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy,
                waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner.transform.position,
                Quaternion.identity);
            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;
            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            if (_currentWaveIndex < waves.Length - 1)
            {
                _currentWaveIndex++;
                _enemiesLeftToSpawn = waves[_currentWaveIndex].WaveSettings.Length;
                _currentEnemyIndex = 0;
            }
        }
    }

    public void LaunchWave()
    {
        StartCoroutine(SpawnEnemyInWave());
    }
}

[Serializable]
public class Waves
{
    [SerializeField] private WaveSettings[] waveSettings;
    public WaveSettings[] WaveSettings => waveSettings;
}

[Serializable]
 public class WaveSettings
 {
     [SerializeField] private GameObject enemy;
     public GameObject Enemy => enemy;
     [SerializeField] private GameObject neededSpawner;
     public GameObject NeededSpawner => neededSpawner;
     [SerializeField] private float spawnDelay;
     public float SpawnDelay => spawnDelay;
 }
