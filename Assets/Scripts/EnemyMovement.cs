using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private TweenerCore<Vector3, Path, PathOptions> _tweenerCore;
    
    private LevelProvider _levelProvider;
    
    [Inject]
    void Construct(LevelProvider levelProvider)
    {
        _levelProvider = levelProvider;
    }
    
    void Start()
    {
        transform.position = _levelProvider.StartPoint.position;
        StartMovement();
    }

    private void OnDestroy()
    {
        _tweenerCore.Kill();
    }

    private void StartMovement()
    {
        _tweenerCore = transform.DOPath(_levelProvider.GetWayPointsVector3(), _moveSpeed, PathType.Linear, PathMode.TopDown2D).SetSpeedBased(true).SetEase(Ease.Linear);
    }

    public async UniTaskVoid UpdateSpeedForSecs(float percentage, float secs)
    {
        _tweenerCore.timeScale = percentage / 100;
        await UniTask.Delay(TimeSpan.FromSeconds(secs));
        _tweenerCore.timeScale = 1;
    }
}
