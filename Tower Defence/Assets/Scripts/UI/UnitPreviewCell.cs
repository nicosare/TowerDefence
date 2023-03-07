using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using static Unit;

public class UnitPreviewCell : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    [SerializeField] private Text description;

    public void SetDescription(Unit unit)
    {
        icon.GetComponent<Image>().sprite = unit.Icon;
        var unitDescription = new StringBuilder();
        if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[1])
        {
            unitDescription.Append("Х »м€: " + unit.NameUnit);
            unitDescription.Append("\r\nХ “ип: " + GetUnitType(unit));
            if (unit.Damage != 0)
                unitDescription.Append("\r\nХ ”рон: " + unit.Damage);
            if (unit.TryGetComponent(out RoadUnit roadUnit))
                unitDescription.Append(roadUnit is Trap ? "\r\nХ ¬рем€ жизни: " + roadUnit.health : "\r\nХ «доровье: " + roadUnit.health);
            unitDescription.Append("\r\nХ —тоимость: " + unit.BuyPrice);
            unitDescription.Append("\r\nХ ќписание: " + unit.Description);
        }
        else
        {
            unitDescription.Append("Х Name: " + unit.NameUnit);
            unitDescription.Append("\r\nХ Type: " + GetUnitType(unit));
            if (unit.Damage != 0)
                unitDescription.Append("\r\nХ Damage: " + unit.Damage);
            if (unit.TryGetComponent(out RoadUnit roadUnit))
                unitDescription.Append(roadUnit is Trap ? "\r\nХ Lifetime: " + roadUnit.health : "\r\nХ HP: " + roadUnit.health);
            unitDescription.Append("\r\nХ Cost: " + unit.BuyPrice);
            unitDescription.Append("\r\nХ Description: " + unit.Description);
        }

        description.text = unitDescription.ToString();
    }

    public string GetUnitType(Unit unit)
    {
        if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[1])
            switch (unit.Type)
            {
                case TypeUnit.Melee:
                    return "Ѕлижний бой";
                case TypeUnit.Wall:
                    return "—тена";
                case TypeUnit.Trap:
                    return "Ћовушка";
                case TypeUnit.Ranged:
                    return "ƒальний бой";
                case TypeUnit.Magician:
                    return "ћаг";
                default:
                    return default;
            }
        else
            switch (unit.Type)
            {
                case TypeUnit.Melee:
                    return "Melee";
                case TypeUnit.Wall:
                    return "Wall";
                case TypeUnit.Trap:
                    return "Trap";
                case TypeUnit.Ranged:
                    return "Ranged";
                case TypeUnit.Magician:
                    return "Magician";
                default:
                    return default;
            }
    }
}
