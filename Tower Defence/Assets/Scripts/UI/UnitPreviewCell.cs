using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
        unitDescription.Append("� ���: " + unit.NameUnit);
        unitDescription.Append("\r\n� ���: " + GetUnitType(unit));
        if (unit.Damage != 0)
            unitDescription.Append("\r\n� ����: " + unit.Damage);
        if (unit.TryGetComponent(out RoadUnit roadUnit))
            unitDescription.Append(roadUnit is Trap ? "\r\n� ����� �����: " + roadUnit.health : "\r\n� ��������: " + roadUnit.health);
        unitDescription.Append("\r\n� ���������: " + unit.BuyPrice);
        unitDescription.Append("\r\n� ��������: " + unit.Description);

        description.text = unitDescription.ToString();
    }

    public string GetUnitType(Unit unit)
    {
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
    }
}
