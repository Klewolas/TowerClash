using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
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

    private void StartMovement()
    {
        transform.DOPath(_levelProvider.GetWayPointsVector3(), _moveSpeed, PathType.Linear, PathMode.TopDown2D).SetSpeedBased(true).SetEase(Ease.Linear);
    }
}
