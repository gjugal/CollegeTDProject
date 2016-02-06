using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Soldier : Entity
{

    Transform gateEnd;
    NavMeshAgent agent;

    protected override void Start()
    {
        gateEnd = GameObject.Find("Gate").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(gateEnd.position + Vector3.up*0.4f);
    }
}
