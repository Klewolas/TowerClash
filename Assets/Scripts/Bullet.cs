using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Object References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Attributes")] 
    [SerializeField] private float _bulletSpeed;
    
    private Transform _target;
    private float _bulletDamage;

    public void SetProperties(Transform target, int bulletDamage)
    {
        _target = target;
        _bulletDamage = bulletDamage;
    }
    
    private void FixedUpdate()
    {
        if (!_target)
        {
            Debug.Log("Bullet : Target is null bullet destroy self.");
            Destroy(gameObject);
            return;
        }

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
        if (_target.gameObject.GetInstanceID() != other.gameObject.GetInstanceID()) return;
        other.gameObject.GetComponent<Health>().TakeDamage(_bulletDamage);
        Destroy(gameObject);
    }
}
