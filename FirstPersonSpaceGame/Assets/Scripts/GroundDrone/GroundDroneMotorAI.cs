using UnityEngine;
using UnityEngine.AI;

public class GroundDroneMotorAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent groundDroneNavMeshAgent;
    public Transform npcObject;
    private const float minDistance = 5f;
    

    void Start()
    {
        groundDroneNavMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Vector3 newY = groundDroneNavMeshAgent.transform.position;
        newY.y = newY.y + 3;
        groundDroneNavMeshAgent.transform.position = newY;
    }
    public void FollowPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < minDistance)
        {
            // Calculate a destination away from the player
            Vector3 destination = transform.position - directionToPlayer.normalized * (minDistance - distanceToPlayer);
            groundDroneNavMeshAgent.SetDestination(destination);
        }
        else
        {
            groundDroneNavMeshAgent.SetDestination(player.transform.position);
        }

    }
    public void FindNPCS(string npcTag)
    {
        if (npcObject == null)
        {
            // Find the npc
            GameObject[] npcs = GameObject.FindGameObjectsWithTag(npcTag);
            float minDist = Mathf.Infinity;
            foreach (GameObject npc in npcs)
            {
                float dist = Vector3.Distance(transform.position,npc.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    npcObject = npc.transform;
                }
            }
        }

        if (npcObject != null)
        {
            // Move the AI to the npc object
            groundDroneNavMeshAgent.SetDestination(npcObject.position);
        }
    }
    public void ResetNPCS()
    {
        npcObject = null;
    }
}
