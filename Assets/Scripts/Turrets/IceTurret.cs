using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class IceTurret : TurretController
{
    [SerializeField] private float _freezeTime;

    private void Start()
    {
        Shoot();
    }

    protected override async UniTaskVoid StartAttack()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f / AttackPerSecond));
        Shoot();
    }

    protected override void Shoot()
    {
        GameObject attackObj = Instantiate(AttackPrefab, FirePoint.position, Quaternion.identity);
        IceAttack iceAttack = attackObj.GetComponent<IceAttack>();
        iceAttack.SetProperties(_freezeTime, 50);
        StartAttack().Forget();
        // FreezeEnemies();
    }

    // private void FreezeEnemies()
    // {
    //     RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, TargetingRange, (Vector2)transform.position,
    //         0f, EnemyMask);
    //     
    //     if (hits.Length > 0)
    //     {
    //         for (var i = 0; i < hits.Length; i++)
    //         {
    //             hits[i].transform.GetComponent<EnemyMovement>().UpdateSpeedForSecs(50, _freezeTime).Forget();
    //         }
    //     }
    //     
    //     StartAttack().Forget();
    // }
}