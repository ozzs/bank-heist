using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMotion : MonoBehaviour
{

    private NavMeshAgent agent;
    public GameObject target;

    private LineRenderer lines;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        lines = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //sets targets and starts motion towards the target




        if (agent.enabled)
        {
            agent.SetDestination(target.transform.position);

            lines.positionCount = agent.path.corners.Length;
            lines.SetPositions(agent.path.corners);

        }

       
        

    }



}
