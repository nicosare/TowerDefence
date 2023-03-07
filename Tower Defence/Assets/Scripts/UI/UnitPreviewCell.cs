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
            unitDescription.Append("� ���: " + unit.NameUnit);
            unitDescription.Append("\r\n� ���: " + GetUnitType(unit));
            if (unit.Damage != 0)
                unitDescription.Append("\r\n� ����: " + unit.Damage);
            if (unit.TryGetComponent(out RoadUnit roadUnit))
                unitDescription.Append(roadUnit is Trap ? "\r\n� ����� �����: " + roadUnit.health : "\r\n� ��������: " + roadUnit.health);
            unitDescription.Append("\r\n� ���������: " + unit.BuyPrice);
            unitDescription.Append("\r\n� ��������: " + unit.Description);
        }
        else
        {
            unitDescription.Append("� Name: " + unit.NameUnit);
            unitDescription.Append("\r\n� Type: " + GetUnitType(unit));
            if (unit.Damage != 0)
                unitDescription.Append("\r\n� Damage: " + unit.Damage);
            if (unit.TryGetComponent(out RoadUnit roadUnit))
                unitDescription.Append(roadUnit is Trap ? "\r\n� Lifetime: " + roadUnit.health : "\r\n� HP: " + roadUnit.health);
            unitDescription.Append("\r\n� Cost: " + unit.BuyPrice);
            unitDescription.Append("\r\n� Description: " + unit.Description);
        }

        description.text = unitDescription.ToString();
    }

    public string GetUnitType(Unit unit)
    {
        if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[1])
            switch (unit.Type)
            {
                case TypeUnit.Melee:
                    return "������� ���";
                case TypeUnit.Wall:
                    return "�����";
                case TypeUnit.Trap:
                    return "�������";
                case TypeUnit.Ranged:
                    return "������� ���";
                case TypeUnit.Magician:
                    return "���";
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
