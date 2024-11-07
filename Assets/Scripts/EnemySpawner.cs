using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    private IInstantiator _instantiator;

    [SerializeField] private GameObject _enemyPrefab;
    
    //TODO : LevelConfig ve installer içine alınacak leveller buradn çekilecek.
    [Header("Wave Attribute")]
    [SerializeField] private int _waveEnemyCount = 8;
    [SerializeField] private float _enemyPerSecond = 0.2f;
    [SerializeField] private float _timeBetweenWaves = 0.5f;
    [SerializeField] private int _waveCount = 5;

    public UnityAction OnEnemyDestroyed;
    
    [Inject]
    void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }
    
    private void Start()
    {
        SpawnEnemies().Forget();
    }

    private async UniTaskVoid SpawnEnemies()
    {
        int currentWave = 1;

        while (currentWave <= _waveCount)
        {
            int currentEnemyCount = 0;

            while (currentEnemyCount <= _waveEnemyCount)
            {
                _instantiator.InstantiatePrefab(_enemyPrefab);
                
                await UniTask.Delay(TimeSpan.FromSeconds(_enemyPerSecond));
                currentEnemyCount++;
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(_timeBetweenWaves));
            currentWave++;
        }
    }
}