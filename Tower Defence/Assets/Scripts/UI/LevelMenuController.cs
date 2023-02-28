using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    [Header("Controllers")]
    public int panOffset;
    [Range(0f, 200f)]
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
    [SerializeField] private Image mainTheme;
    [SerializeField] private Text locationName;
    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;
    private Transform[] snapPanels;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanID;
    private bool isScrolling;
    private int panCount;

    private void Start()
    {
        panCount = LevelManager.Instance.Locations.Length;
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];

        InstantiatePans();
    }

    private void InstantiatePans()
    {
        LevelManager.Instance.buttons.Clear();

        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            foreach (Transform button in instPans[i].transform)
            {
                LevelManager.Instance.buttons.Add(button.GetComponent<Button>());
            }
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                instPans[i].transform.localPosition.y);
            pansPos[i] = -instPans[i].transform.localPosition;
        }
        LevelManager.Instance.UpdateButtons();
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
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
                UpdateInfo();
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > sensitivity) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void SnapToPanel(int panelID)
    {
        contentVector.x = pansPos[panelID].x;
        contentRect.anchoredPosition = contentVector;
    }

    public void SnapWithPanelsToPrevious()
    {
        if (selectedPanID != 0)
            SnapToPanel(selectedPanID - 1);
    }
    public void SnapWithPanelsToNext()
    {
        if (selectedPanID != panCount - 1)
            SnapToPanel(selectedPanID + 1);
    }

    private void ScrollWithWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            SnapToPanel((selectedPanID + 1) % 3);

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedPanID != 0)
                SnapToPanel((selectedPanID - 1) % 3);
            else
                SnapToPanel(panCount - 1);
        }
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    private void UpdateInfo()
    {
        mainTheme.color = LevelManager.Instance.Locations[selectedPanID].BGColor;
        locationName.text = LevelManager.Instance.Locations[selectedPanID].LocationName;
    }
}
