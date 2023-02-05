using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCastle : MonoBehaviour, IHealth
{
    [Range(0, 500)]
    [SerializeField] private int health;
    [SerializeField] private Text healthUIText;
    [SerializeField] private Button UltimateAttackButton;
    [SerializeField] private Way[] Ways;
    [SerializeField] private UltimateBullet ultimateBullet;
    [SerializeField] private int cooldownTime;
    public WindowsController windowsController;

    private void Start()
    {
        PrintHealthInUI();
    }
    public int Health { get => health; set => health = value; }

    public void Die()
    {
        Time.timeScale = 0;
        windowsController.SetActiveDefeatMenu(true);
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        PrintHealthInUI();
        if (health <= 0)
            Die();
    }

    private void PrintHealthInUI()
    {
        healthUIText.text = health.ToString();
    }

    public void UltimateAttack()
    {
        foreach (var way in Ways)
        {
            var bullet = Instantiate(ultimateBullet.gameObject);
            bullet.transform.position = new Vector3(transform.position.x,
                                                             .35f,
                                                             transform.position.z);
            bullet.transform.SetParent(transform);
            bullet.GetComponent<UltimateBullet>().WayPoints = way.WayPoints;
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        UltimateAttackButton.interactable = false;
        yield return new WaitForSeconds(cooldownTime);
        UltimateAttackButton.interactable = true;
    }
}
