using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Soldier : Entity
{

    public float soldier_health = 5f;
    Transform gateEnd;
    Transform currentTarget = null;

    NavMeshAgent agent;
    enum State { WALK, ATTACK };

    List<Transform> activeTargets;
    int currentTargetIndex = 0;
    State currentState;
    Color originalColor;
    float timeBetweenShoots = 1;
    float lastShootTime = 0;
    float damage = 1;

    protected override void Start()
    {
        base.Start();
        health = soldier_health;
        gateEnd = GameObject.Find("Gate").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(gateEnd.position + Vector3.up*0.4f);

        base.Start();
        activeTargets = new List<Transform>();
        activeTargets = GameObject.Find("GameManager").GetComponent<GameManager>().GetListOfActiveTarget();
        gateEnd = GameObject.Find("Gate").GetComponent<Transform>();
        activeTargets.Add(gateEnd);
        if (currentTargetIndex < activeTargets.Count)
        {
            agent = GetComponent<NavMeshAgent>();
            SetTarget();
        }
        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
        Debug.Log(originalColor);

    }

    void SetTarget()
    {
        currentTarget = activeTargets[currentTargetIndex];
        if (currentTarget.gameObject.tag == "Tower")
        {
            Entity towerEntity = currentTarget.gameObject.GetComponent<Entity>();
            towerEntity.OnDeath += ChangeTarget;
        }
        currentState = State.WALK;
        agent.enabled = true;
        agent.SetDestination(currentTarget.position + Vector3.up * 0.4f);
       // this.gameObject.GetComponent<Renderer>().material.color = originalColor;
    }

    public void ChangeTarget()
    {
        currentTargetIndex++;
        bool targetFound = false;
        if (currentTargetIndex < activeTargets.Count)
        {
            while (currentTargetIndex < activeTargets.Count && !targetFound)
            {
                if (activeTargets[currentTargetIndex].gameObject.activeSelf)
                {
                    SetTarget();
                    targetFound = true;
                }
                else
                {
                    currentTargetIndex++;
                }
            }
        }
    }

    void Update()
    {
        if (currentState == State.ATTACK)
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "TowerBase")
        {
            agent.enabled = false;
            currentState = State.ATTACK;
        }
    }

    void Shoot()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        currentTarget.gameObject.GetComponent<Entity>().TakeDamage(damage);
    }


}
