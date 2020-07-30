using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs = null;
    [SerializeField] int startingWaveIndex = 0;
    [SerializeField] bool looping = false;

    
    IEnumerator Start()
    {
        do{ yield return StartCoroutine(SpawnAllWaves());}
        while (looping);
    }

    private IEnumerator SpawnAllEnemies(WaveConfig waveConfig)
    {
        for(int enemyCount = 1; enemyCount <=  waveConfig.GetEnemyNumber(); enemyCount++)
        {
            var enemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
                );
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnInterval() + waveConfig.GetRandomer());
        }
      
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIdx = startingWaveIndex; waveIdx < waveConfigs.Count; waveIdx++)
        {
            var currentWave = waveConfigs[waveIdx];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }
}
