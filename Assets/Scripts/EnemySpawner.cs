using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Min/Max List Size")]
    [SerializeField] int waveRandomMin = 0;
    [SerializeField] int waveRandomMax = 5;
    int waveRandom;

    [Header("Toggle Looping")]
    [SerializeField] bool looping = true;

    [Header("Waves")]
    [SerializeField] List<WaveConfig> waveConfigs;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnWaves()
    {
        waveRandom = Random.Range(waveRandomMin, waveRandomMax);
        var currentWave = waveConfigs[waveRandom];
        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    //Add a limit of waves, counter variable that is being tested against. Stops wave and start boss.
    //Cycle waves? 5x normal waves, 1x major wave, repeat. Counter variable?

    public void StopWaves()
    {
        looping = false;
        //may need a new method to restart the waves.
    }

    //Original System
    /*[SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveCount = startingWave; waveCount < waveConfigs.Count; waveCount++)
        {
            var currentWave = waveConfigs[waveCount];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++) 
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }


    }*/
}
