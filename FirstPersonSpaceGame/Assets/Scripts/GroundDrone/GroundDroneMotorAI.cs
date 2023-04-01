using UnityEngine;
using UnityEngine.AI;

public class GroundDroneMotorAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    public Transform treeObject;
    private const float minDistance = 2f;
    

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void FollowPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < minDistance)
        {
            // Calculate a destination away from the player
            Vector3 destination = transform.position - directionToPlayer.normalized * (minDistance - distanceToPlayer);
            navMeshAgent.SetDestination(destination);
        }
        else
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
    public void FindTree()
    {
        if (treeObject == null)
        {
            // Find the closest tree object
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            float minDist = Mathf.Infinity;
            foreach (GameObject tree in trees)
            {
                float dist = Vector3.Distance(transform.position, tree.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    treeObject = tree.transform;
                }
            }
        }

        if (treeObject != null)
        {
            // Move the AI to the tree object
            navMeshAgent.SetDestination(treeObject.position);
        }
    }
    public void ResetTrees()
    {
        treeObject= null;
    }
}
