using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GunTurret : TurretController
{
    [Header("Gun Turret Object References")]
    [SerializeField] protected Transform _turretRotationPoint;
    
    private Transform _target;
    
    private void Start()
    {
        FindTargetAndShooting();
    }
    
    private async void FindTargetAndShooting()
    {
        _target = await FindTarget();

        StartAttack().Forget();
    }
    
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(_target.position, transform.position) <= TargetingRange;
    }
    
    protected override async UniTaskVoid StartAttack()
    {
        _target.gameObject.TryGetComponent(out Health targetHealth);
        while (_target != null)
        {
            RotateTowardsTarget();

            if (!CheckTargetIsInRange())
            {
                _target = null;
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f / AttackPerSecond));
                if (targetHealth.CanShoot(TurretAttackDamage))
                    Shoot();
            }
        }

        FindTargetAndShooting();
    }
    
    private void RotateTowardsTarget()
    {
        float angle =
            Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) *
            Mathf.Rad2Deg - 90f;
        
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        _turretRotationPoint.rotation = targetRotation;
    }

    protected override void Shoot()
    {
        GameObject bulletObj = Instantiate(AttackPrefab, FirePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetProperties(_target, TurretAttackDamage);
    }

    private async UniTask<Transform> FindTarget()
    {
        Transform target = null;
        while (target == null)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, TargetingRange, (Vector2)transform.position,
                0f, EnemyMask);

            if (hits.Length > 0)
            {
                target = hits[0].transform;
            }

            await UniTask.Delay(TimeSpan.FromMilliseconds(15));
        }

        return target;
    }
}