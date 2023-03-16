using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class WaveSpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawners;
    [SerializeField] private List<Wave> waves;
    [SerializeField] private int timeBetweenSpawn;
    [SerializeField] private int startTime;
    [SerializeField] private Text timerText;
    [SerializeField] private Text WaveText;

    private int enemiesCounter;
    private int wavesCounter;
    private int countWave;
    private int timer;
    private List<GameObject> enemies;
    private bool isWin;

    public WindowsController windowsController;
    public SoundButton sound;

    private void Awake()
    {
        wavesCounter = 0;
        timer = startTime;
        enemiesCounter = waves.Sum(wave => wave.Enemies.Count) * spawners.Count;
        countWave = waves.Count();
        PrintNumberWave();
        Time.timeScale = 1;
    }

    private void Start()
    {
        isWin = false;
        Debug.Log(enemiesCounter);
        enemies = new List<GameObject>();
        StartCoroutine(SpawnWaves(waves, timeBetweenSpawn));
    }

    private void Update()
    {
        if (!isWin)
            CheckPlayerWin();
    }

    private void CheckPlayerWin()
    {
        if (enemies.Count == enemiesCounter && enemies.All(enemy => enemy.IsDestroyed()))
        {
            sound.ChangeSoundsEffect();
            isWin = true;
            Time.timeScale = 0;
            if (SceneManager.GetActiveScene().name.Contains("15")
                && !Progress.Instance.CheckIsCompletedFractionByName(FactionsManager.Instance.ChoosenFaction.name))
                windowsController.SetActiveWinMenuWithAchievment(true);
            else
                windowsController.SetActiveWinMenu(true);
            if (SceneManager.GetActiveScene().name != "Level_Tutorial")
                LevelManager.Instance.UnblockLevel(SceneManager.GetActiveScene());
        }
    }

    IEnumerator SpawnWaves(List<Wave> waves, int timeBetweenSpawn)
    {
        StartCoroutine(Timer());
        yield return new WaitForSeconds(startTime);

        foreach (var wave in waves)
        {
            wavesCounter++;
            PrintNumberWave();
            timer = wave.TimeBetweenSpawn * wave.Enemies.Count + timeBetweenSpawn;

            if (wavesCounter != waves.Count)
                StartCoroutine(Timer());
            foreach (var enemy in wave.Enemies)
            {
                foreach (var spawner in spawners)
                {
                    var newEnemy = Instantiate(enemy.gameObject);
                    newEnemy.transform.position = new Vector3(spawner.position.x,
                                                             .35f,
                                                             spawner.position.z);
                    newEnemy.transform.SetParent(spawner);

                    enemies.Add(newEnemy);
                }
                yield return new WaitForSeconds(wave.TimeBetweenSpawn);
            }
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    IEnumerator Timer()
    {
        for (var i = timer; i >= 0; i--)
        {
            timerText.text = i.ToString();

            if (i == 0)
                if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[0])
                    timerText.text = wavesCounter == waves.Count ? "Final wave!" : wavesCounter + " wave!";
                else
                    timerText.text = wavesCounter == waves.Count ? "Финальная волна!" : wavesCounter + "-я волна!";
            yield return new WaitForSeconds(1);
        }
    }

    private void PrintNumberWave()
    {
        WaveText.text = String.Format("{0}/{1}", wavesCounter, countWave);
    }
}
