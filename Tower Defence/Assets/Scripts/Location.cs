using UnityEngine;
using UnityEngine.Localization.Settings;

[CreateAssetMenu(fileName = "New Location", order = 3)]
public class Location : ScriptableObject
{
    [SerializeField]private string locationNameRu;
    [SerializeField]private string locationNameEn;
    public string LocationName { get => LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[0] ? locationNameEn : locationNameRu; }
}
