using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private Sprite languageRuImage;
    [SerializeField] private Sprite languageEnImage;
    [SerializeField] private Button selectLanguageButton;


    private void Awake()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
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
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            selectLanguageButton.image.sprite = languageEnImage;
            SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[0]);
        }
        else
        {
            selectLanguageButton.image.sprite = languageRuImage;
            SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[1]);
        }
    }
}
