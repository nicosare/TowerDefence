using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float startTime;
    [SerializeField] private Text timerText;
    private int timer;

    private void Start()
    {
        timer = (int)startTime;
        StartCoroutine(SpawnWaves(waves, timeBetweenSpawn));
    }

    IEnumerator SpawnWaves(List<Wave> waves, float timeBetweenSpawn)
    {
        StartCoroutine(Timer());
        yield return new WaitForSeconds(startTime);

        foreach (var wave in waves)
        {
            timer = (int)wave.TimeBetweenSpawn * wave.Enemies.Count + (int)timeBetweenSpawn;
            StartCoroutine(Timer());
            foreach (var enemy in wave.Enemies)
            {
                var newEnemy = Instantiate(enemy.gameObject);
                newEnemy.transform.position = new Vector3(transform.position.x, .25f, transform.position.z);
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
                timerText.text = "Новая волна!";
            yield return new WaitForSeconds(1);
        }
    }
}
