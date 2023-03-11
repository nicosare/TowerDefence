using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField] private GameObject[] hiddenBlocks;
    [SerializeField] private GameObject[] openedBlock;
    public bool stopTime;
    public bool InGameInputUnitCell;
    public bool InGameInputPlace;
    public bool InGameInputUpgrade;
    public bool InGameInputSell;
    public bool InGameInputEnemyIsGone;
    public bool InGameInputUnitDeath;
    public bool InGameInputUltimateAttack;
    public bool InGameInputEndUltimate;
    public bool KillSwitchReason;
    public int NextTime;
    public bool AfterMessage;
    public bool AfterEnemyGo;

    public void SetParameters()
    {
        foreach (var block in hiddenBlocks)
            block.SetActive(false);
        foreach (var block in openedBlock)
            block.SetActive(true);
        Time.timeScale = stopTime ? 0 : 1;
    }
}
