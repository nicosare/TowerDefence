using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Runtime.InteropServices;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private Sprite languageRuImage;
    [SerializeField] private Sprite languageEnImage;
    [SerializeField] private Button selectLanguageButton;
    private string currentLanguage;

    [DllImport("__Internal")]
    private static extern string GetLang();

    private void Awake()
    {
        currentLanguage = GetLang();
        if (currentLanguage == "ru")
        {
            selectLanguageButton.image.sprite = languageRuImage;
            SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[1]);
        }
        else
        {
            selectLanguageButton.image.sprite = languageEnImage;
            SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[0]);
        }
    }

    private void SetSelectedLocale(Locale locale)
    {
        LocalizationSettings.SelectedLocale = locale;
    }

    public void ChangeLanguage()
    {
        if (currentLanguage == "ru")
        {
            currentLanguage = "en";
            selectLanguageButton.image.sprite = languageEnImage;
            SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[0]);
        }
        else
        {
            currentLanguage = "ru";
            selectLanguageButton.image.sprite = languageRuImage;
            SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[1]);
        }
    }
}
