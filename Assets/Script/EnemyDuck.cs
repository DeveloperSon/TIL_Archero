using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDuck : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent navMesh;

    private void Update()
    {
        navMesh.SetDestination(player.position);
    }
}
