using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(NavMeshAgent))]
public class King : Entity
{
    public float kingHealth;
    enum KingStates { IDLE, WALK, ATTACK };
    NavMeshAgent agent;
    KingStates currentState;
    Camera mainCamera;
    public LayerMask pathLayerMask;

    float timeBetweenShoots = 1;
    float lastShootTime = 0;
    float damage = 1;
    Color originalColor;

    LinkedList<Transform> targets = null;
    Transform currentTarget = null;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        health = kingHealth;
        currentState = KingStates.IDLE;
        agent = GetComponent<NavMeshAgent>();
        targets = new LinkedList<Transform>();
        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
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
                //Debug.Log(final);
                agent.SetDestination(final + Vector3.up * 0.4f);
                currentState = KingStates.WALK;
            }
            //clickPos1.z = 10;
            //Vector3 clickPos = Camera.main.ScreenToWorldPoint(clickPos1);

        }

        if (currentState == KingStates.ATTACK && currentTarget != null)
        {
            if (Time.time > lastShootTime + timeBetweenShoots)
            {
                lastShootTime = Time.time;
                Shoot();
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = originalColor;
            }
        }

    }

    void OnTriggerEnter(Collider col) {
        try
        {
            if (col.gameObject.tag == "TowerBase")
            {
                targets.AddLast(col.gameObject.transform);
                if(currentTarget == null)
                {
                    currentTarget = targets.First.Value;
                    currentState = KingStates.ATTACK;
                }
            }
        }
        catch { }
    }

    void OnTriggerExit(Collider col) {
        try
        {
            if (currentTarget == col.gameObject.transform)
            {
                currentTarget = null;
                targets.Remove(col.gameObject.transform);
                if (targets.Count > 0)
                {
                    currentTarget = targets.First.Value;
                }
                else {
                    currentState = KingStates.IDLE;
                }
            }
        }
        catch { }
    }

    void Shoot() {
        Debug.Log(currentTarget);
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        currentTarget.gameObject.GetComponent<Entity>().TakeDamage(damage);
    }
}
