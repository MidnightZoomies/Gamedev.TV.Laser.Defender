using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    int startingWayPoints = 1;
    float enrageLevel = 1f;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig) //not the same as the variable
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        var targetPosition = waypoints[waypointIndex].transform.position;
        var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime * enrageLevel;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        if (transform.position == targetPosition)
        {
            if (waypointIndex <= startingWayPoints + 1)
            {
                waypointIndex++;
            }
            else
            {
                waypointIndex--;
            }
        }
    }
    public void BossPartDestroyed()
    {
        enrageLevel += 0.25f;
    }
}
