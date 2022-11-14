using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VendingNPC : MonoBehaviour
{

    private bool reachedPosition = false;
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject currentPosition;
    public GameObject[] positions;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.SetDestination(currentPosition.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetInteger("state") != 0)
        {
            if (reachedPosition == false)
            {
                reachingDes();
            }
            else
            {
                nextQueuePosition();
            }
        }
        else
        {
            agent.enabled = false;
        }

    }

    void nextQueuePosition()
    {
        if (agent.isStopped)
        {
            int currentIndex = System.Array.IndexOf(positions, currentPosition);
            agent.isStopped = false;
            animator.SetInteger("state", 1);
            if (positions[currentIndex] == positions[positions.Length - 1])
            {
                agent.SetDestination(positions[0].transform.position);
                currentPosition = positions[0];
            }
            else
            {
                agent.SetDestination(positions[currentIndex + 1].transform.position);
                currentPosition = positions[currentIndex + 1];
            }
        }
        reachedPosition = false;
    }


    void reachingDes()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    StartCoroutine("waitInLine");
                }
            }
        }
    }

    IEnumerator waitInLine()
    {
        agent.isStopped = true;
        animator.SetInteger("state", 2);
        yield return new WaitForSeconds(2f);
        reachedPosition = true;
    }
}
