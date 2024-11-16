using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class CurrencyTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyText;

    private Tweener _textTween;
    
    private GameCurrencyManager _gameCurrencyManager;
    
    [Inject]
    void Construct(GameCurrencyManager gameCurrencyManager)
    {
        _gameCurrencyManager = gameCurrencyManager;
    }
    
    void Start()
    {
        _gameCurrencyManager.OnCurrencyChanged += UpdateCurrencyText;

        SetUI();
    }

    private void OnDestroy()
    {
        _gameCurrencyManager.OnCurrencyChanged -= UpdateCurrencyText;
    }

    private void SetUI()
    {
        _currencyText.text = _gameCurrencyManager.Currency.ToString();
    }

    private void UpdateCurrencyText(int currency)
    {
        if (_textTween != null && _textTween.IsPlaying())
        {
            _textTween.Kill();
        }
        TextAnimation(int.Parse(_currencyText.text), currency);
    }

    private void TextAnimation(int startValue, int endValue)
    {
        _textTween = DOTween.To(x => _currencyText.text = Mathf.Round(x).ToString(), startValue, endValue, 0.5f)
            .SetEase(Ease.OutCubic);
    }
}
