using UnityEngine;
using UnityEngine.AI;

public class GroundDroneMotorAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent groundDroneNavMeshAgent;
    public Transform treeObject;
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
            groundDroneNavMeshAgent.SetDestination(treeObject.position);
        }
    }
    public void ResetTrees()
    {
        treeObject= null;
    }
}
