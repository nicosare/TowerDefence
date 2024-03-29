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
    [SerializeField] private UnitOnFieldMenu unitOnFieldMenu;
    [SerializeField] private Button boostButton;
    [SerializeField] private GameObject clickText;
    [SerializeField] private GameObject placeArrow;
    private bool canUpdateUnitCells = true;
    [SerializeField] private Place archerPlace;
    [SerializeField] private Place swordsmanPlace;
    private BoxCollider archerCollider;

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
            if (currentSlideIndex + 1 == 7)
                archerPlace.isFree = true;
            if (currentSlideIndex + 1 == 24)
                swordsmanPlace.isFree = true;
            if (currentSlideIndex + 1 == 34)
            {
                BackgroundMusicController.Instance.ChangeMainMusic();
                FindObjectsOfType<Place>().All(place => place.isFree = true);
            }
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
    public void NextSlideWithPlace(Transform unit)
    {
        if (slides[currentSlideIndex].InGameInputPlace)
        {
            archerCollider = unit.GetChild(0).GetComponent<BoxCollider>();
            NextSlide();
        }
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
        if (currentSlideIndex == 7)
        {
            placeArrow.SetActive(true);
            placeArrow.transform.position = new Vector3(-3, 0.5f, -3.25f);

        }
        else if (currentSlideIndex == 24)
        {
            placeArrow.SetActive(true);
            placeArrow.transform.position = new Vector3(2, 0.5f, -2.25f);
        }
        else
            placeArrow.SetActive(false);

        clickText.SetActive(slides[currentSlideIndex].ShowClickText);
        if (currentSlideIndex < 34)
        {
            boostButton.interactable = false;
            if (archerCollider != null
                && currentSlideIndex != 11
                && currentSlideIndex != 14
                && currentSlideIndex != 17
                && currentSlideIndex != 21)
            {
                unitOnFieldMenu.MenuSetActive(false);
                archerCollider.enabled = false;
            }
            else if (archerCollider != null)
                archerCollider.enabled = true;

            if (currentSlideIndex != 21)
                unitOnFieldMenu.transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
            else
                unitOnFieldMenu.transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = true;
        }
        else
        {
            boostButton.interactable = true;
            unitOnFieldMenu.transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = true;
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
