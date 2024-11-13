using UnityEngine;
using Zenject;

public class Health : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private GameCurrencyManager _gameCurrencyManager;
 
    [SerializeField] private float _healthPoint;
    [SerializeField] private int _currencyWorth;

    private float _actualHealthPoint;
    private bool _targetWillDestroyed;
    
    [Inject]
    void Construct(EnemySpawner enemySpawner, GameCurrencyManager gameCurrencyManager)
    {
        _enemySpawner = enemySpawner;
        _gameCurrencyManager = gameCurrencyManager;
        
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
            OnDestroyActions();
            Destroy(gameObject);
        }
    }

    public void OnDestroyActions()
    {
        _gameCurrencyManager.EarnCurrency(_currencyWorth);
    }
}