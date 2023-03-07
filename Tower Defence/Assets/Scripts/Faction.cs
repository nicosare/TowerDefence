using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Faction", order = 2)]
public class Faction : ScriptableObject
{
    public Sprite IconFaction;
    [SerializeField] private string nameFactionRu;
    [SerializeField] private string nameFactionEn;
    [SerializeField] private string descriptionFactionRu;
    [SerializeField] private string descriptionFactionEn;
    public List<Unit> Units;
    public UltimateBullet UltimateBullet;
    public string NameFaction { get => LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[0] 
            ? nameFactionEn : nameFactionRu; }
    public string DescriptionFaction { get => LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[0] 
            ? descriptionFactionEn : descriptionFactionRu; }
    public Color MainColor;
}
