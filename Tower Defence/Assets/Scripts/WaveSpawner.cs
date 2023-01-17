using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private float timeBetweenSpawn;
    
    private void Start()
    {
        StartCoroutine(SpawnWaves(waves, timeBetweenSpawn));
    }


    IEnumerator SpawnWaves(List<Wave> waves, float timeBetweenSpawn)
    {
        yield return new WaitForSeconds(timeBetweenSpawn);

        foreach(var wave in waves)
        {
            foreach(var enemy in wave.Enemies)
            {
                var newEnemy = Instantiate(enemy.gameObject);
                newEnemy.transform.position = new Vector3(transform.position.x, .25f, transform.position.z);
                yield return new WaitForSeconds(wave.TimeBetweenSpawn);
            }
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
