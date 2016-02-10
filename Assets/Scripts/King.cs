using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class King : Entity
{
    public float kingHealth;
    enum KingStates { IDLE, WALK, ATTACK };
    NavMeshAgent agent;
    KingStates currentState;
    Camera mainCamera;
    public LayerMask pathLayerMask;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        health = kingHealth;
        currentState = KingStates.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(clickPos);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            RaycastHit rayhit;
            if (Physics.Raycast(ray.origin, ray.direction * 10, out rayhit, 500, pathLayerMask))
            {
                Vector3 final = rayhit.point;
                Debug.Log(final);
                agent.SetDestination(final + Vector3.up * 0.4f);
                currentState = KingStates.WALK;
            }
            //clickPos1.z = 10;
            //Vector3 clickPos = Camera.main.ScreenToWorldPoint(clickPos1);

        }
    }
}
