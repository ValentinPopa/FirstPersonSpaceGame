using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private float distance = 30;
    private NavMeshAgent bearNavMeshAgent;
    Vector3 spawnLocation;
    void Start()
    {
        bearNavMeshAgent = GetComponent<NavMeshAgent>();
        spawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if(distanceToPlayer<=distance)
        {
            bearNavMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            bearNavMeshAgent.SetDestination(spawnLocation);
        }
        if(distanceToPlayer<=3)
        {
            PlayerState.Instance.currentHealth -= 5;
        }
    }
}
