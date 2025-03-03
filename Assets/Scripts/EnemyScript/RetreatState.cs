using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : MonoBehaviour, BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Memulai Patrol");
    }

    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Patrolling");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Menghentikan Patrolling");
    }
}
