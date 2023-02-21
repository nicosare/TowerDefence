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
    [SerializeField] private Image MainTheme;

    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;
    private FactionsManager factionsManager;

    private int panCount;
    private int selectedPanID;
    private bool isScrolling;
    private bool canMove = false;

    private void Start()
    {
        factionsManager = FactionsManager.Instance;
        panCount = factionsManager.Factions.Length;
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            instPans[i].GetComponent<Image>().sprite = factionsManager.Factions[i].IconFaction;
            instPans[i].transform.GetChild(0).GetComponent<Text>().text = factionsManager.Factions[i].NameFaction;

            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i].transform.localPosition.x,
                instPans[i - 1].transform.localPosition.y - panPrefab.GetComponent<RectTransform>().sizeDelta.y - panOffset);
            pansPos[i] = -instPans[i].transform.localPosition;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            SnapToPanel((selectedPanID + 1) % 4);

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedPanID != 0)
                SnapToPanel((selectedPanID - 1) % 4);
            else
                SnapToPanel(panCount - 1);
        }

        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.y - pansPos[i].y);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
                factionsManager.ChoosenFaction = factionsManager.Factions[selectedPanID];
                MainTheme.color = factionsManager.ChoosenFaction.MainColor;
                UpdateIndicator();
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < sensitivity && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > sensitivity) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, pansPos[selectedPanID].y, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void SnapToPanel(int panelID)
    {
        contentVector.y = pansPos[panelID].y;
        contentRect.anchoredPosition = contentVector;
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
                indicator.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, .25f);
                indicator.GetChild(i).localScale = new Vector2(.8f, .8f);
            }
        }
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll)
            scrollRect.inertia = true;
    }
}
