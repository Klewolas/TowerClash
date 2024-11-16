using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CurrencyTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyText;
    
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
        _currencyText.text = currency.ToString();
    }

    
}
