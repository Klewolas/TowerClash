using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _towerPrefabs;

    private int _selectedTower = 0;

    public GameObject GetSelectedTower()
    {
        return _towerPrefabs[_selectedTower];
    }
}