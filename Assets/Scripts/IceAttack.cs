using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    private float _freezeTime;
    private float _freezePercentage;
    
    void Start()
    {
        StartAnimation();
    }

    public void StartAnimation()
    {
        transform.DOScale(new Vector3(2f, 2f, 2f), 2f).OnComplete(() => 
            Destroy(gameObject));
    }

    public void SetProperties(float freezeTime, float freezePercentage)
    {
        _freezeTime = freezeTime;
        _freezePercentage = freezePercentage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<EnemyMovement>().UpdateSpeedForSecs(_freezePercentage, _freezeTime).Forget();
    }
}
