using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopTurretButtonUI : MonoBehaviour
{
    [SerializeField] private Turret _turret;
    [SerializeField] private TMP_Text _turretNameText;
    [SerializeField] private Button _button;

    
    private BuildManager _buildManager;
    
    [Inject]
    void Construct(BuildManager buildManager)
    {
        _buildManager = buildManager;
    }
    
    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(() => _buildManager.SetSelectedTurret(_turret));
    }
}