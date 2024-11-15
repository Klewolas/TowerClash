using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public abstract class TurretController : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] protected LayerMask EnemyMask;
    [SerializeField] protected GameObject AttackPrefab;
    [SerializeField] protected Transform FirePoint;
    
    [Header("Attribute")]
    [SerializeField] protected float TargetingRange;
    [SerializeField] protected float AttackPerSecond;
    [SerializeField] protected int TurretAttackDamage;

    protected abstract UniTaskVoid StartAttack();

    protected abstract void Shoot();

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, TargetingRange);
    }
}
