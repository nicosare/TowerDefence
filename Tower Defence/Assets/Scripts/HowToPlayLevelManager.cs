using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HowToPlayLevelManager : MonoBehaviour
{
    [SerializeField] private Faction faction;
    [SerializeField] private WaveSpawnManager spawnManager;
    [SerializeField] private Slide[] slides;
    [SerializeField] private Transform unitCellsMenu;
    [SerializeField] private int currentSlideIndex;
    [SerializeField] private List<GameObject> unitCells;
    [SerializeField] private GameObject unitOnFieldMenu;
    [SerializeField] private Button boostButton;
    private bool canUpdateUnitCells = true;

    private void Awake()
    {
        FactionsManager.Instance.ChoosenFaction = faction;
        currentSlideIndex = 0;
    }

    private void Start()
    {
        slides[currentSlideIndex].gameObject.SetActive(true);
        slides[currentSlideIndex].SetParameters();
    }


    public void NextSlide()
    {
        if (currentSlideIndex != slides.Length - 1)
        {
            if (currentSlideIndex == 5)
            {
                unitCells[0].SetActive(true);
                unitCells[1].SetActive(true);
            }
            slides[currentSlideIndex].gameObject.SetActive(false);
            slides[currentSlideIndex + 1].gameObject.SetActive(true);
            slides[currentSlideIndex + 1].SetParameters();
            var time = slides[currentSlideIndex + 1].NextTime;
            if (time > 0)
                StartCoroutine(NextSlideAfterTime(time));
            currentSlideIndex++;
        }
    }

    public void NextSlideWithUpgrade()
    {
        if (slides[currentSlideIndex].InGameInputUpgrade)
            NextSlide();
    }
    public void NextSlideWithSell()
    {
        if (slides[currentSlideIndex].InGameInputSell)
            NextSlide();
    }
    public void NextSlideWithPlace()
    {
        if (slides[currentSlideIndex].InGameInputPlace)
            NextSlide();
    }
    public void NextSlideWithUnitCell()
    {
        if (slides[currentSlideIndex].InGameInputUnitCell)
            NextSlide();
    }
    public void NextSlideWithEnemyIsGone()
    {
        if (slides[currentSlideIndex].InGameInputEnemyIsGone)
            NextSlide();
    }
    public void NextSlideWithUnitDeath()
    {
        if (slides[currentSlideIndex].InGameInputUnitDeath)
            NextSlide();
    }
    public void NextSlideWithUltimateAttack()
    {
        if (slides[currentSlideIndex].InGameInputUltimateAttack)
            NextSlide();
    }
    public void NextSlideWithEndUltimate()
    {
        if (slides[currentSlideIndex].InGameInputEndUltimate)
            NextSlide();
    }

    private void Update()
    {
        if (currentSlideIndex < 33)
        {
            boostButton.interactable = false;
            if (currentSlideIndex != 11 && currentSlideIndex != 14 && currentSlideIndex != 17)
            {
                unitOnFieldMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
            }
            if (currentSlideIndex != 20)
                unitOnFieldMenu.transform.GetChild(0).GetComponent<Button>().interactable = false;
            else
                unitOnFieldMenu.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
        else
        {
            boostButton.interactable = true;
            unitOnFieldMenu.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
        if (canUpdateUnitCells)
            if (currentSlideIndex == 0)
            {
                foreach (Transform unitCell in unitCellsMenu)
                {
                    unitCells.Add(unitCell.gameObject);
                    unitCell.gameObject.SetActive(false);
                }
                canUpdateUnitCells = false;
            }
        if (slides[currentSlideIndex].KillSwitchReason && FindObjectsOfType<Enemy>().All(enemy => enemy.IsDestroyed()))
            NextSlide();
        if (slides[currentSlideIndex].AfterMessage && FindObjectOfType<Message>().transform.GetChild(0).gameObject.activeSelf)
            NextSlide();
        if (slides[currentSlideIndex].AfterEnemyGo && !(FindObjectsOfType<Enemy>().Length == 0))
            NextSlide();
    }

    IEnumerator NextSlideAfterTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        NextSlide();
    }
}
