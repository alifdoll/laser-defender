using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] GameObject pathPrefab = null;
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] float spawnRandomer = 0.5f;
    [SerializeField] int numberOfEnemy = 10;
    [SerializeField] float movementSpeed = 2f;

    public GameObject GetEnemyPrefab(){ return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var wayPoint = new List<Transform>();

        foreach(Transform child in pathPrefab.transform)
        {
            wayPoint.Add(child);
        }
        return wayPoint;
    }

    public float GetSpawnInterval() { return spawnInterval; }

    public float GetRandomer() { return Random.Range(0.1f, spawnRandomer); }
    
    public float GetMoveSpeed() { return movementSpeed; }

    public int GetEnemyNumber() { return numberOfEnemy; }

}
