using UnityEngine;
using Zenject;

public class Health : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
 
    [SerializeField] private float _healthPoint;

    private float _actualHealthPoint;
    private bool _targetWillDestroyed;
    
    [Inject]
    void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
        _actualHealthPoint = _healthPoint;
    }

    public bool CanShoot(float dmg)
    {
        if (_actualHealthPoint >= dmg)
        {
            _actualHealthPoint -= dmg;
            return true;
        }
        return false;
    }
    
    public void TakeDamage(float dmg)
    {
        _healthPoint -= dmg;

        if (_healthPoint <= 0)
        {
            _enemySpawner.OnEnemyDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}