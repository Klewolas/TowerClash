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
    private IInstantiator _instantiator;

    private GameObject _placeableObject;
    private Color _startColor;
    
    [Inject]
    void Construct(BuildManager buildManager, IInstantiator instantiator)
    {
        _buildManager = buildManager;
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

        _placeableObject = _instantiator.InstantiatePrefab(_buildManager.GetSelectedTower(), transform.position,
            Quaternion.identity, transform);
    }
}
