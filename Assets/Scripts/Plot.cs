using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _hoverColor;

    private BuildManager _buildManager;
    private GameCurrencyManager _gameCurrencyManager;
    private IInstantiator _instantiator;

    private GameObject _placeableObject;
    private Color _startColor;
    
    [Inject]
    void Construct(BuildManager buildManager, GameCurrencyManager gameCurrencyManager, IInstantiator instantiator)
    {
        _buildManager = buildManager;
        _gameCurrencyManager = gameCurrencyManager;
        _instantiator = instantiator;

        _startColor = _spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = _startColor;
    }

    private void OnMouseDown()
    {
        if (_placeableObject != null) return;

        var turret = _buildManager.GetSelectedTurret();

        if (turret.Cost > _gameCurrencyManager.Currency)
        {
            Debug.Log("You can't afford this turret.");
            return;
        }

        _gameCurrencyManager.SpendCurrency(turret.Cost);
        _placeableObject = _instantiator.InstantiatePrefab(turret.TurretPrefab, transform.position,
            Quaternion.identity, transform);
    }
}
