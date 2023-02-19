using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Faction", order = 2)]
public class Faction : ScriptableObject
{
    public Sprite IconFaction;
    public string NameFaction;
    public List<Unit> Units;
    public UltimateBullet UltimateBullet;
    public Color MainColor;
}
