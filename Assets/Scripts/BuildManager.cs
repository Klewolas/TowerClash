using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private Turret[] _turrets;

    private int _selectedTurret = 0;

    public Turret GetSelectedTurret()
    {
        return _turrets[_selectedTurret];
    }

    public void SetSelectedTurret(int selectedTurret)
    {
        _selectedTurret = selectedTurret;
    }
}