using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class enemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    void Start ()
    {
        enemy = GetComponent<Enemy>();

        if(enemy.Fly)
        {
            target = Waypoints.points[Waypoints.points.Length-1];
            wavepointIndex = Waypoints.points.Length-1;
        }
        else
        {
            target = Waypoints.points[0];
        }
    }


    void Update ()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        if(enemy.TakeShortcut == true)
        {
            if(wavepointIndex <= Waypoints.points.Length - 2)
            {
                wavepointIndex++;
            }
            enemy.TakeShortcut = false;
        }


        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length -1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    void EndPath()
    {
        PlayerStats.Lives -= enemy.enemyDamage;
        Destroy(gameObject);
    }
}
