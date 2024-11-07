using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField] private EnemySpawner _enemySpawner;
    public override void InstallBindings()
    {
        Container.BindInstance(_enemySpawner).AsSingle().NonLazy();
    }
}
