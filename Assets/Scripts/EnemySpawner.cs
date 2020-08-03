using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<WaveConfig>[] arrayOfWaves;
    [Header("Waves")]
    [SerializeField] List<WaveConfig> firstWave = null;
    [SerializeField] List<WaveConfig> secondWave = null;
    [SerializeField] List<WaveConfig> thirdWave = null;
    [SerializeField] List<WaveConfig> bossWave = null;

    [Header("Debugger")]
    [SerializeField] int enemyNumber = 0;
    [SerializeField] [Range(1,4)] int currentWaves = 1;
    [SerializeField] int numberOfArray = 0;



    void Start()
    {
        SetUpArrayOfWaves();
        enemyNumber = GetEnemyNumInWave(arrayOfWaves[currentWaves]);
        StartCoroutine(SpawnWave(arrayOfWaves[currentWaves]));
        
    }

    private void Update()
    {
        if(currentWaves > numberOfArray)
        {
            enemyNumber = 0;
            FindObjectOfType<LevelLoader>().LoadPlayerWin();
        }
      

        if (enemyNumber <= 0)
        {
            currentWaves++;
            enemyNumber = GetEnemyNumInWave(arrayOfWaves[currentWaves]);
            StartCoroutine(SpawnWave(arrayOfWaves[currentWaves]));
        }
    }

    private void SetUpArrayOfWaves()
    {
        arrayOfWaves = new List<WaveConfig>[4]
        {
            firstWave,
            secondWave,
            thirdWave,
            bossWave
        };

        numberOfArray = arrayOfWaves.Length;
    }


   

    private IEnumerator SpawnAllEnemies(WaveConfig waveConfig)
    {
        int numberOfEnemy = waveConfig.GetEnemyNumber();
        for (int enemyCount = 1; enemyCount <= numberOfEnemy; enemyCount++)
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



    private IEnumerator SpawnWave(List<WaveConfig> wave)
    {
        for (int waveIdx = 0; waveIdx < wave.Count; waveIdx++)
        {
            var currentWave = wave[waveIdx];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }


    public int GetEnemyNumInWave(List<WaveConfig> waveList)
    {
        int enemyNumber = 0;
        foreach(WaveConfig waves in waveList)
        {
            enemyNumber += waves.GetEnemyNumber();
        }
        return enemyNumber;
    }

   

    public void EnemyDecreased()
    {
        enemyNumber--;
        if(enemyNumber <= 0)
        {
            //FindObjectOfType<LevelLoader>().LoadNextLevel();
        }
    }

}
