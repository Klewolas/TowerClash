using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ShopTurretButtonListener : MonoBehaviour
{
    [SerializeField] private int _turretIndex;

    private Button _button;
        
    private BuildManager _buildManager;
    
    [Inject]
    void Construct(BuildManager buildManager)
    {
        _buildManager = buildManager;
    }
    
    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(() => _buildManager.SetSelectedTurret(_turretIndex));
    }
}