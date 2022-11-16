using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    };
    
    [Serializable]
    public class Wave
    {
        public string Name;
        public Transform Enemy;
        public int Count;
        public float Rate;
    }

    public Wave[] Waves;
    private int _nextWave = 0;

    public Transform[] SpawnPoints;
    
    public float TimeBetweenWaves = 5f;
    private float _waveCountdown;

    private float _searchCountdown = 1f;

    private SpawnState _state = SpawnState.Counting;

    private void Start()
    {
        if (SpawnPoints.Length == 0)
        {
            Debug.Log("No spawn points referenced.");
        }
        
        _waveCountdown = TimeBetweenWaves;
    }

    private void Update()
    {
        if (_state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        if (_waveCountdown <= 0)
        {
            if (_state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(Waves[_nextWave]));
            }
        }
        else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        _state = SpawnState.Counting;
        _waveCountdown = TimeBetweenWaves;

        if (_nextWave + 1 > Waves.Length - 1)
        {
            _nextWave = 0;
            Debug.Log("All Waves Complete! Looping...");
        }
        else
        {
            _nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        _searchCountdown -= Time.deltaTime;
        if (_searchCountdown <= 0f)
        {
            _searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.Name);
        _state = SpawnState.Spawning;

        for (int i = 0; i < _wave.Count; i++)
        {
            SpawnEnemy(_wave.Enemy);
            yield return new WaitForSeconds(1f / _wave.Rate);
        }
        
        _state = SpawnState.Waiting;
        
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
    }
}
