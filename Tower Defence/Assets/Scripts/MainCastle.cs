using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCastle : MonoBehaviour, IHealth
{
    [Range(0, 500)]
    [SerializeField] private int health;
    [SerializeField] private Text healthUIText;
    [SerializeField] private Button UltimateAttackButton;
    [SerializeField] private Way[] Ways;
    [SerializeField] private int cooldownTime;
    public WindowsController windowsController;
    private UltimateBullet ultimateBullet;
    private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    public SoundButton sound;
    private Slider progressBar;
    private bool isCooldownUltimateAttack;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        progressBar = UltimateAttackButton.transform.GetChild(0).GetComponent<Slider>();
        ultimateBullet = FactionsManager.Instance.ChoosenFaction.UltimateBullet;
        PrintHealthInUI();
    }

    private void Update()
    {
        if (progressBar.value == progressBar.minValue && !isCooldownUltimateAttack)
        {
            isCooldownUltimateAttack = false;
        }
        else if (isCooldownUltimateAttack)
        {
            progressBar.value = progressBar.minValue + progressBar.maxValue - Time.time;
        }
    }
    public int Health { get => health; set => health = value; }

    public void Die()
    {
        sound.ChangeSoundsEffect();
        Time.timeScale = 0;
        windowsController.SetActiveDefeatMenu(true);
    }

    public void GetDamage(int damage)
    {
        audioSource.PlayOneShot(hurtSound);
        StartCoroutine(Counting(damage));
    }

    private void PrintHealthInUI()
    {
        healthUIText.text = health.ToString();
    }

    IEnumerator Counting(int damage)
    {
        for (; damage > 0; damage--)
        {
            Health -= 1;
            if (Health <= 0)
                Die();
            PrintHealthInUI();

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void UltimateAttack()
    {
        if (SceneManager.GetActiveScene().name == "Level_Tutorial")
            FindObjectOfType<HowToPlayLevelManager>().NextSlideWithUltimateAttack();

        foreach (var way in Ways)
        {
            var bullet = Instantiate(ultimateBullet.gameObject);
            bullet.transform.position = new Vector3(transform.position.x,
                                                             .35f,
                                                             transform.position.z);
            bullet.transform.SetParent(transform);
            bullet.GetComponent<UltimateBullet>().WayPoints = way.WayPoints;
        }
        isCooldownUltimateAttack = true;
        progressBar.minValue = Time.time;
        progressBar.maxValue = Time.time + cooldownTime;
        progressBar.value = progressBar.maxValue;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        UltimateAttackButton.interactable = false;
        yield return new WaitForSeconds(cooldownTime);
        UltimateAttackButton.interactable = true;
    }
}
