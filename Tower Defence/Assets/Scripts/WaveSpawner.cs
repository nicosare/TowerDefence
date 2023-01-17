using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private int timeBetweenSpawn;
    [SerializeField] private int startTime;
    [SerializeField] private Text timerText;

    private int wavesCounter;
    private int timer;

    private void Start()
    {
        wavesCounter = 0;
        timer = startTime;
        StartCoroutine(SpawnWaves(waves, timeBetweenSpawn));
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
                newEnemy.transform.position = new Vector3(transform.position.x, .35f, transform.position.z);
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
            {
                timerText.text = wavesCounter == waves.Count ? "Финальная волна!" : wavesCounter + "-я волна!";
            }
            yield return new WaitForSeconds(1);
        }
    }
}
