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
    [SerializeField] List<WaveConfig> slowWaveConfigs;
    [SerializeField] List<WaveConfig> fastWaveConfigs;
    [SerializeField] List<WaveConfig> toughWaveConfigs;
    [SerializeField] WaveConfig miniBossWaveConfig;
    [SerializeField] WaveConfig bossWaveConfig;

    [Header("Wave Variables")]
    int toughWaveCountDown = 1;
    int bossCountDown = 1;
    [SerializeField] int toughWaveRate = 3;
    [SerializeField] int bossRate = 20;
    [SerializeField] int bossRateDefault = 20;
    int toughWaveRandom;
    int fastWaveRandom;
    int slowWaveRandom;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] float spawnDelayMin = 0f;
    [SerializeField] float spawnDelayMax = 5f;
    bool miniBossDefeated = false;
    [SerializeField] float bossSpawnDelay = 10f;
    [SerializeField] float startWait = 5f;

    private void Start()
    {
        StartCoroutine(SpawnLooping());
    }

    private IEnumerator SpawnLooping()
    {
        yield return new WaitForSeconds(startWait);
        do
        {
            yield return StartCoroutine(SpawnWaves());
            toughWaveCountDown++;
            bossCountDown++;
            if (bossCountDown >= bossRate && miniBossDefeated == false)
            {
                bossRate = 10000;
                StartCoroutine(SpawnBoss(miniBossWaveConfig));
            }
            else if (bossCountDown >= bossRate && miniBossDefeated == true)
            {
                StopWaves();
                StartCoroutine(SpawnBoss(bossWaveConfig));
            }
        }
        while (looping);
    }

    private IEnumerator SpawnWaves()
    {
        slowWaveRandom = Random.Range(waveRandomMin, waveRandomMax);
        fastWaveRandom = Random.Range(waveRandomMin, waveRandomMax);
        var currentSlowWave = slowWaveConfigs[slowWaveRandom];
        var currentFastWave = fastWaveConfigs[fastWaveRandom];
        spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
        StartCoroutine(SpawnAllEnemiesInWave(currentSlowWave));
        yield return new WaitForSeconds(spawnDelay);
        yield return StartCoroutine(SpawnAllEnemiesInWave(currentFastWave));
        if (toughWaveCountDown >= toughWaveRate)
        {
            //yield return new WaitForSeconds(spawnDelay); Add if needed
            toughWaveRandom = Random.Range(waveRandomMin, waveRandomMax);
            var currentToughWave = toughWaveConfigs[toughWaveRandom];
            StartCoroutine(SpawnAllEnemiesInWave(currentToughWave));
            toughWaveCountDown = 0; //change to 1?
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
    }

    private IEnumerator SpawnBoss(WaveConfig waveConfig)
    {
        yield return new WaitForSeconds(bossSpawnDelay);
        var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
        newEnemy.GetComponent<BossPathing>().SetWaveConfig(waveConfig);
    }

    public void StopWaves()
    {
        looping = false;
    }

    public void MiniBossDeath()
    {
        bossRate = bossRateDefault;
        miniBossDefeated = true;
        bossCountDown = 1;
    }
}
    /*[Header("Min/Max List Size")]
    [SerializeField] int waveRandomMin = 0;
    [SerializeField] int waveRandomMax = 5;
    int waveRandom;

    [Header("Toggle Looping")]
    [SerializeField] bool looping = true;

    [Header("Waves")]
    [SerializeField] List<WaveConfig> waveConfigs;


    [SerializeField] List<WaveConfig> miniBossWaveConfigs;
    [SerializeField] WaveConfig bossWaveConfig;
    [SerializeField] int miniBossCountDown =1;
    [SerializeField] int bossCountDown = 1;
    [SerializeField] int miniBossRate = 3;
    [SerializeField] int bossRate = 20;
    int miniBossWaveRandom;

    int fastWaveRandom;
    int slowWaveRandom;
    [SerializeField] List<WaveConfig> slowWaveConfigs;
    [SerializeField] List<WaveConfig> fastWaveConfigs;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] float spawnDelayMin = 0f;
    [SerializeField] float spawnDelayMax = 5f;
    //bool miniBossDefeated = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnWaves());
            miniBossCountDown++;
            bossCountDown++;
            //if (bossCountDown >= bossRate && miniBossDefeated == true)
            if (bossCountDown >= bossRate)
            {
                StopWaves();
                StartCoroutine(SpawnBoss(bossWaveConfig));
            }
        }
        while (looping);
    }

    private IEnumerator SpawnWaves()
    {
        slowWaveRandom = Random.Range(waveRandomMin, waveRandomMax);
        fastWaveRandom = Random.Range(waveRandomMin, waveRandomMax);
        var currentSlowWave = slowWaveConfigs[slowWaveRandom];
        var currentFastWave = fastWaveConfigs[fastWaveRandom];
        spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
        StartCoroutine(SpawnAllSlowEnemies(currentSlowWave));
        yield return new WaitForSeconds(spawnDelay);
        yield return StartCoroutine(SpawnAllFastEnemies(currentFastWave));
        //if (miniBossCountDown >= miniBossRate && miniBossDefeated == false)
        if (miniBossCountDown >= miniBossRate)
            {
                miniBossWaveRandom = Random.Range(waveRandomMin, waveRandomMax);
                var currentMiniBossWave = miniBossWaveConfigs[miniBossWaveRandom];
                StartCoroutine(SpawnAllMiniBossesInWave(currentMiniBossWave));
                miniBossCountDown = 0;
            }
    }

    private IEnumerator SpawnAllFastEnemies(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private IEnumerator SpawnAllSlowEnemies(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private IEnumerator SpawnAllMiniBossesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private IEnumerator SpawnBoss(WaveConfig waveConfig)
    {
        yield return new WaitForSeconds(5f);
        var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
        newEnemy.GetComponent<BossPathing>().SetWaveConfig(waveConfig);
    }

        //Add a limit of waves, counter variable that is being tested against. Stops wave and start boss.
        //Cycle waves? 5x normal waves, 1x major wave, repeat. Counter variable?
        public void StopWaves()
        {
        looping = false;
        //may need a new method to restart the waves.
        }*/





    /* Second Changes
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnWaves());
            miniBossCountDown++;
            bossCountDown++;
            if (bossCountDown >= bossRate)
            {
                StopWaves();
                StartCoroutine(SpawnBoss(bossWaveConfig));
            }
        }
        while (looping);
    }

    private IEnumerator SpawnWaves()
    {
        waveRandom = Random.Range(waveRandomMin, waveRandomMax);
        var currentWave = waveConfigs[waveRandom];
        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        if (miniBossCountDown >= miniBossRate)
        {
            miniBossWaveRandom = Random.Range(waveRandomMin, waveRandomMax);
            var currentMiniBossWave = miniBossWaveConfigs[miniBossWaveRandom];
            StartCoroutine(SpawnAllMiniBossesInWave(currentMiniBossWave));
            miniBossCountDown = 0;
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
