using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Soldier : Entity
{

    Transform gateEnd;
    protected Transform currentTarget = null;

    NavMeshAgent agent;
    protected enum States { WALK, SET, ATTACK };//SET is when soldier finds new target and moves towards it for attacking

    LinkedList<Transform> Targets;
    protected States currentState;

    protected override void Start()
    {
        base.Start();
        Targets = new LinkedList<Transform>();
        gateEnd = GameObject.Find("Gate").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(gateEnd.position + Vector3.up*0.4f);
        currentState = States.WALK;
        //base.Start();
        //activeTargets = new List<Transform>();
        //activeTargets = GameObject.Find("GameManager").GetComponent<GameManager>().GetListOfActiveTarget();
        //gateEnd = GameObject.Find("Gate").GetComponent<Transform>();
        //activeTargets.Add(gateEnd);
        //if (currentTargetIndex < activeTargets.Count)
        //{
        //    agent = GetComponent<NavMeshAgent>();
        //    SetTarget();
        //}
        
        //Debug.Log(originalColor);

    }

    

    public void TargetEntry(Transform t, Dictionary<string, int> d) {//called from censor whenever it detects a towerbase
        Debug.Log("TargetEntry called");
        if (CheckCondition(t,d)) {
            Targets.AddLast(t);
            Entity towerEntity = t.gameObject.GetComponent<Entity>();
            towerEntity.OnDeath += ChangeTarget;
            if (currentTarget == null) {
                agent.SetDestination(t.position + Vector3.up * 0.4f);
                currentState = States.SET;
                currentTarget = Targets.First.Value;
                if (t.gameObject.tag == "Tower")
                {
                    currentTarget.gameObject.GetComponent<Tower>().AddToAttackingSoldiers(this.transform);//add this soldier to dict. of currenttarget tower
                }
                else if (t.gameObject.tag == "BlockBarricade") {
                    currentTarget.gameObject.GetComponent<BlockBarricade>().AddToAttackingSoldiers(this.transform);
                }
            }
        }
    }

    protected abstract bool CheckCondition(Transform t, Dictionary<string, int> d);

    void ChangeTarget(Transform t) {//registered to OnDeath event of towers enrolled in this soldier
        if (currentTarget == t) {
            Debug.Log("change target called");
            currentTarget = null;
        }
        Targets.Remove(t);
        
        if (Targets.Count > 0) {
            foreach (Transform eachTarget in Targets) {
                Debug.Log("eachtarget" + eachTarget);
                if (CheckCondition(eachTarget, eachTarget.GetComponent<Tower>().attackingSoldiers))
                {

                    Debug.Log("count is > 0");
                    currentTarget = Targets.First.Value;
                    currentState = States.SET;
                    if (t.gameObject.tag == "Tower")
                    {
                        currentTarget.gameObject.GetComponent<Tower>().AddToAttackingSoldiers(this.transform);//add this soldier to dict. of currenttarget tower
                    }
                    else if (t.gameObject.tag == "BlockBarricade")
                    {
                        currentTarget.gameObject.GetComponent<BlockBarricade>().AddToAttackingSoldiers(this.transform);
                    }
                    agent.SetDestination(currentTarget.position + Vector3.up * 0.4f);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("count is < 0");
            currentState = States.WALK;
            agent.SetDestination(gateEnd.position + Vector3.up * 0.4f);
        }
    }

    

    

    

    //void SetTarget()
    //{
    //    currentTarget = activeTargets[currentTargetIndex];
    //    if (currentTarget.gameObject.tag == "Tower")
    //    {
    //        Entity towerEntity = currentTarget.gameObject.GetComponent<Entity>();
    //        towerEntity.OnDeath += ChangeTarget;
    //    }
    //    currentState = State.WALK;
    //    //agent.enabled = true;
    //    if (agent != null)
    //    {
    //        agent.SetDestination(currentTarget.position + Vector3.up * 0.4f);
    //    }
    //   // this.gameObject.GetComponent<Renderer>().material.color = originalColor;
    //}

    //public void ChangeTarget(Transform t)
    //{
    //    currentTargetIndex++;
    //    bool targetFound = false;
    //    if (currentTargetIndex < activeTargets.Count)
    //    {
    //        while (currentTargetIndex < activeTargets.Count && !targetFound)
    //        {
    //            if (activeTargets[currentTargetIndex].gameObject.activeSelf)
    //            {
    //                SetTarget();
    //                targetFound = true;
    //            }
    //            else
    //            {
    //                currentTargetIndex++;
    //            }
    //        }
    //    }
    //}

}
