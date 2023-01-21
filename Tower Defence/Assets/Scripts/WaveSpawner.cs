using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private int timeBetweenSpawn;
    [SerializeField] private int startTime;
    [SerializeField] private Text timerText;
    [SerializeField] private Way Way;

    private int enemiesCounter;
    private int wavesCounter;
    private int timer;
    private List<GameObject> enemies;
    public List<Transform> WayPoints;

    private void Awake()
    {
        WayPoints = Way.WayPoints;
        wavesCounter = 0;
        timer = startTime;
        enemiesCounter = waves.Sum(wave => wave.Enemies.Count);
    }

    private void Start()
    {
        Debug.Log(enemiesCounter);
        enemies = new List<GameObject>();
        StartCoroutine(SpawnWaves(waves, timeBetweenSpawn));
    }

    private void Update()
    {
        if (enemies.Count == enemiesCounter && enemies.All(enemy => enemy.IsDestroyed()))
            Message.Instance.LoadMessage("Победа!");
    }

    IEnumerator SpawnWaves(List<Wave> waves, int timeBetweenSpawn)
    {
        StartCoroutine(Timer());
        yield return new WaitForSeconds(startTime);

        foreach (var wave in waves)
        {
            wavesCounter++;
            timer = wave.TimeBetweenSpawn * wave.Enemies.Count + timeBetweenSpawn;

            if (wavesCounter != waves.Count)
                StartCoroutine(Timer());

            foreach (var enemy in wave.Enemies)
            {
                var newEnemy = Instantiate(enemy.gameObject);
                newEnemy.transform.position = new Vector3(transform.position.x,
                                                         .35f,
                                                         transform.position.z);
                newEnemy.transform.SetParent(transform);

                enemies.Add(newEnemy);
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
                timerText.text = wavesCounter == waves.Count ? "Финальная волна!" : wavesCounter + "-я волна!";
            yield return new WaitForSeconds(1);
        }
    }
}
