using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionMenuController : MonoBehaviour
{
    [Header("Controllers")]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Range(1f, 500f)]
    public float sensitivity;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;
    [SerializeField] private Transform indicator;
    [SerializeField] private Image mainTheme;
    [SerializeField] private Text description;
    [SerializeField] private Transform unitPreviewPanel;
    [SerializeField] private GameObject unitPreviewCell;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject infoAndBuyButton;
    [SerializeField] private GameObject separator;

    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;
    private FactionsManager factionsManager;

    private int panCount;
    private int selectedPanID;
    private bool isScrolling;
    private bool canScrollWithWheel;
    private int previousPanID;
    private int targetPanel;
    private bool canSkipPanel;


    private void Start()
    {
        canSkipPanel = false;
        previousPanID = -1;
        factionsManager = FactionsManager.Instance;
        panCount = factionsManager.Factions.Length;
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];

        InstantiatePans();
    }

    private void Update()
    {
        ScrollWithWheel();

        if ((contentRect.anchoredPosition.y >= pansPos[0].y
            || contentRect.anchoredPosition.y <= pansPos[pansPos.Length - 1].y)
            && !isScrolling)
            scrollRect.inertia = false;

        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            var distance = Mathf.Abs(contentRect.anchoredPosition.y - pansPos[i].y);

            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }

            var scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
        }

        if (selectedPanID != previousPanID)
        {
            previousPanID = selectedPanID;
            if (!canSkipPanel && targetPanel != selectedPanID)
                targetPanel = selectedPanID;
            if (targetPanel == selectedPanID)
                canSkipPanel = false;
            UpdateInfo();
        }

        var scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < sensitivity && !isScrolling)
            scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > sensitivity)
            return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, pansPos[targetPanel].y, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    private void ScrollWithWheel()
    {
        if (canScrollWithWheel)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && targetPanel != panCount - 1)
                SnapToPanel(selectedPanID + 1);

            if (Input.GetAxis("Mouse ScrollWheel") > 0 && targetPanel != 0)
            {
                SnapToPanel(selectedPanID - 1);
            }
        }
    }

    private void InstantiatePans()
    {
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            instPans[i].GetComponent<Image>().sprite = factionsManager.Factions[i].IconFaction;
            instPans[i].transform.GetChild(0).GetComponent<Text>().text = factionsManager.Factions[i].NameFaction;

            if (Progress.Instance.CheckBanFractionByName(factionsManager.Factions[i].NameFaction))
                instPans[i].transform.GetChild(1).gameObject.SetActive(true);
            else
                instPans[i].transform.GetChild(1).gameObject.SetActive(false);

            if (i == 0)
                continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i].transform.localPosition.x,
                instPans[i - 1].transform.localPosition.y - panPrefab.GetComponent<RectTransform>().sizeDelta.y - panOffset);
            pansPos[i] = -instPans[i].transform.localPosition;
        }
    }

    private void UpdateInfo()
    {
        factionsManager.ChoosenFaction = factionsManager.Factions[selectedPanID];
        mainTheme.color = factionsManager.ChoosenFaction.MainColor;
        ChangeButtons();
        UpdateIndicator();
        UpdateDescription();
    }

    public void SnapToPanel(int panelID)
    {
        canSkipPanel = true;
        targetPanel = panelID;
    }

    private void UpdateIndicator()
    {
        for (int i = 0; i < panCount; i++)
        {
            if (i == selectedPanID)
            {
                indicator.GetChild(i).GetComponent<Image>().color = factionsManager.ChoosenFaction.MainColor;
                indicator.GetChild(i).localScale = new Vector2(1.2f, 1.2f);
            }
            else
            {
                indicator.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                indicator.GetChild(i).localScale = new Vector2(.8f, .8f);
            }
        }
    }

    private void UpdateDescription()
    {
        description.text = factionsManager.ChoosenFaction.DescriptionFaction;
        UpdateUnitCells();
    }

    private void UpdateUnitCells()
    {
        ClearUnitCells();
        foreach (var unit in factionsManager.ChoosenFaction.Units)
        {
            Instantiate(separator, unitPreviewPanel);
            var newUnitPreviewCell = Instantiate(unitPreviewCell, unitPreviewPanel);
            newUnitPreviewCell.GetComponent<UnitPreviewCell>().SetDescription(unit);
        }
        Instantiate(separator, unitPreviewPanel);
    }

    private void ClearUnitCells()
    {
        foreach (Transform cell in unitPreviewPanel)
            Destroy(cell.gameObject);
    }

    public void CanScrollWithWheel(bool canScroll)
    {
        canScrollWithWheel = canScroll;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll)
            scrollRect.inertia = true;
    }

    private void ChangeButtons()
    {
        if (Progress.Instance.CheckBanFractionByName(factionsManager.ChoosenFaction.NameFaction))
        {
            startGameButton.SetActive(false);
            infoAndBuyButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(true);
            infoAndBuyButton.SetActive(false);
        }
    }
}
