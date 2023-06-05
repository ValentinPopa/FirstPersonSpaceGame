using UnityEngine;
using UnityEngine.AI;

public class GroundDroneObjectFinderAI : MonoBehaviour
{
    public Transform treeObject;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
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
            agent.SetDestination(treeObject.position);
        }
    }
}





