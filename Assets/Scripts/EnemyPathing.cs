using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    WaveConfig waveConfig = null;
    List<Transform> waypoints = null;
    int waypointIdx = 0;

    
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIdx].transform.position;
    }

   
    void Update()
    {
        MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveEnemy()
    {
        if (waypointIdx <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIdx].transform.position;
            var movement = waveConfig.GetMoveSpeed()* Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movement);

            if (transform.position == targetPosition)
            {
                waypointIdx++;
            }
        }

        else
        {
            var startingPoint = waypoints[0].transform.position;
            transform.position = startingPoint;
            waypointIdx = 0;
        }
    }
}
