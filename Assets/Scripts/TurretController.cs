using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private Transform _turretRotationPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    
    [Header("Attribute")]
    [SerializeField] private float _targetingRange;
    [SerializeField] private float _bulletPerSecond;
    [SerializeField] private int _turretBulletDamage;

    private Transform _target;
    private float _timeUntilFire;

    private void Start()
    {
        FindTargetAndShooting();
    }

    private async void FindTargetAndShooting()
    {
        _target = await FindTarget();

        StartShoot().Forget();
    }

    private async UniTaskVoid StartShoot()
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
                await UniTask.Delay(TimeSpan.FromSeconds(1f / _bulletPerSecond));
                if (targetHealth.CanShoot(_turretBulletDamage))
                    Shoot();
            }
        }

        FindTargetAndShooting();
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetProperties(_target, _turretBulletDamage);
    }

    private void RotateTowardsTarget()
    {
        float angle =
            Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) *
            Mathf.Rad2Deg - 90f;
        
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        _turretRotationPoint.rotation = targetRotation;
    }
    
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(_target.position, transform.position) <= _targetingRange;
    }

    private async UniTask<Transform> FindTarget()
    {
        Transform target = null;
        while (target == null)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetingRange, (Vector2)transform.position,
                0f, _enemyMask);

            if (hits.Length > 0)
            {
                target = hits[0].transform;
            }

            await UniTask.Delay(TimeSpan.FromMilliseconds(15));
        }

        return target;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetingRange);
    }
}
