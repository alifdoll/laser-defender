using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs = null;
    [SerializeField] int startingWaveIndex = 0;
    [SerializeField] bool looping = false;
    int enemyNumber = 0;

    
    IEnumerator Start()
    {
        do{ yield return StartCoroutine(SpawnAllWaves());}
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIdx = startingWaveIndex; waveIdx < waveConfigs.Count; waveIdx++)
        {
            var currentWave = waveConfigs[waveIdx];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemies(WaveConfig waveConfig)
    {
        enemyNumber = waveConfig.GetEnemyNumber();
        for (int enemyCount = 1; enemyCount <= enemyNumber; enemyCount++)
        {
            var enemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
                );
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(Random.Range(waveConfig.GetSpawnInterval(), waveConfig.GetRandomer()));
        }
       
    }

    public int GetEnemyCount()
    {
        int enemyNumber = 0;
        foreach(WaveConfig waves in waveConfigs)
        {
            enemyNumber += waves.GetEnemyNumber();
        }
        return enemyNumber;
    }

}
