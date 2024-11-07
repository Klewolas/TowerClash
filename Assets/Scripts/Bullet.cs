using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Object References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Attributes")] 
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage;
    
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private void FixedUpdate()
    {
        if (!_target) return;

        RotateTowardsTarget();
        
        Vector2 direction = (_target.position - transform.position).normalized;
        _rigidbody2D.velocity = direction * _bulletSpeed;
    }
    
    private void RotateTowardsTarget()
    {
        float angle =
            Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) *
            Mathf.Rad2Deg - 90f;
        
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        transform.rotation = targetRotation;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(_bulletDamage);
        Destroy(gameObject);
    }
}
