using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private Language _language;
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdown.value = _language.LanguageInt;
    }

    public void ValueChanged()
    {
        _language.ChangeLanguage(_dropdown.value);
    }
}
