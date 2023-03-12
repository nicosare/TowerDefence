using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Place : MonoBehaviour
{
    public bool isFree;
    private Transform spawnPoint;
    public Unit UnitOnPlace;
    private Transform unitPreviewOutline;
    private Interact interact;
    private void Awake()
    {
        interact = FindObjectOfType<Interact>();
        isFree = true;
        spawnPoint = transform.GetChild(1).transform;
        unitPreviewOutline = transform.GetChild(0);
        unitPreviewOutline.gameObject.SetActive(false);
    }

    public void SetUnit(Unit unit)
    {
        var newUnit = Instantiate(unit.gameObject);

        newUnit.transform.SetParent(transform);
        newUnit.transform.position = spawnPoint.position + Vector3.up * 0.5f;
        UnitOnPlace = newUnit.GetComponent<Unit>();
        UnitOnPlace.PlaySoundInstallation();
        isFree = false;
        if (SceneManager.GetActiveScene().name == "HowToPlayLevel")
            FindObjectOfType<HowToPlayLevelManager>().NextSlideWithPlace();
    }

    public void Preview(Unit unit)
    {
        unitPreviewOutline.gameObject.SetActive(true);
        unitPreviewOutline.transform.localScale = new Vector3(2 * unit.AttackRange + 1,
                                                       0.05f,
                                                       2 * unit.AttackRange + 1);
    }


    private void Update()
    {
        if (!UnitOnFieldMenu.Instance.isOpened || (!UnitOnFieldMenu.Instance.isOpened && interact.UnitToPreview == null))
            unitPreviewOutline.gameObject.SetActive(false);
    }
}
