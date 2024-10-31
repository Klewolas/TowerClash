using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _targetingRange;
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetingRange);
    }
}
