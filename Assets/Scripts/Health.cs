using UnityEngine;
using Zenject;

public class Health : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
 
    [SerializeField] private float _healthPoint;
    
    [Inject]
    void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
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