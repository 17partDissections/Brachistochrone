using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public int LanguageInt;
    public Action<int> LanguageHasBeenChanged;
    private void Awake()
    {
        LanguageInt = PlayerPrefs.GetInt("Language");
    }
    public void ChangeLanguage(int  language)
    {
        LanguageInt = language;
        PlayerPrefs.SetInt("Language", LanguageInt);
        LanguageHasBeenChanged.Invoke(LanguageInt);
    }
}
