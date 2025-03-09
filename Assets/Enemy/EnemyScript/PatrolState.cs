using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool _IsMoving;
    private Vector3 _destination;
   public void EnterState(Enemy enemy)
    {
        _IsMoving = false;
        enemy.animator.SetTrigger("PatrolState");
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwtichState(enemy.ChaseState);
        }
        if (!_IsMoving)
        {
            _IsMoving = true;
            //Random index pada waypoint dari 0 sampai 7
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);
            _destination = enemy.Waypoints[index].position;
            enemy.NavMeshAgent.destination = _destination;
        }
        else
        {
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _IsMoving = false;
            }
        }

    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Menghentikan Patrolling");
    }
}
