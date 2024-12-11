using UnityEngine;
using TMPro;
using System;
using Zenject;

public class LanguageText : MonoBehaviour
{
    private Language _languageClass;

    [SerializeField] private int _language;
    [SerializeField] private string[] text;
    private TextMeshProUGUI _textLine;
    private bool _firstInit;
    [Inject] private void Construct(Language language) {  _languageClass = language; }

    void Awake()
    {
        _languageClass.LanguageHasBeenChanged += ChangeLanguage;
        _textLine = GetComponent<TextMeshProUGUI>();
        ChangeLanguage(_languageClass.LanguageInt);
    }
    private void OnEnable() { ChangeLanguage(_languageClass.LanguageInt); }
    private void ChangeLanguage(int language)
    {
        language = PlayerPrefs.GetInt("language", language);
        _language = language;
        _textLine.text = "" + text[_language];
    }
}