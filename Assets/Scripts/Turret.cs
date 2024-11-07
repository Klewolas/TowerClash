using System;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private Transform _turretRotationPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    
    [Header("Attribute")]
    [SerializeField] private float _targetingRange;
    [SerializeField] private float _bulletPerSecond;

    private Transform _target;
    private float _timeUntilFire;
    
    //TODO : They will convert to UniTask.
    private void Update()
    {
        if (_target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            _target = null;
        }
        else
        {
            _timeUntilFire += Time.deltaTime;
            if (_timeUntilFire >= 1f / _bulletPerSecond)
            {
                Shoot();
                _timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetTarget(_target);
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

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetingRange, (Vector2)transform.position,
            0f, _enemyMask);

        if (hits.Length > 0)
        {
            _target = hits[0].transform;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetingRange);
    }
}
