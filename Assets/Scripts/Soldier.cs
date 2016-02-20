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

    protected States currentState;

    protected override void Start()
    {
        base.Start();
        myFirstName = "Soldier";
        entityLL = new LinkedList<MyTargets>();
        gateEnd = GameObject.Find("Gate").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(gateEnd.position + Vector3.up * 0.4f);
        currentState = States.WALK;
    }



    public void TargetEntry(Transform t, int[] entities_count)
    {//called from censor whenever it detects a towerbase or barricade
        if (CheckCondition(t, entities_count))
        {
            Entity defenseEntity = t.gameObject.GetComponent<Entity>();
            defenseEntity.OnDeath += ChangeTarget;
            entityLL.AddLast(new MyTargets(t, false, defenseEntity.myFirstName));
            Debug.Log("Soldier's next target added : " + defenseEntity.myFirstName);
            if (currentTarget == null)
            {
                agent.SetDestination(t.position + Vector3.up * 0.4f);
                currentState = States.SET;
                currentTarget = entityLL.First.Value.GetTransfrom();

                Debug.Log("Target entry" + t.gameObject.tag);
                if (t.gameObject.tag == "Tower")
                {
                    currentTarget.gameObject.GetComponent<Tower>().AddToAttackingEntity(this.transform);//add this soldier to dict. of currenttarget tower
                }
                else if (t.gameObject.tag == "BlockBarricade")
                {
                    Debug.Log("barricade added");
                    currentTarget.gameObject.GetComponent<BlockBarricade>().AddToAttackingSoldiers(this.transform);
                }
            }
        }
    }

    protected abstract bool CheckCondition(Transform t, int[] entities_count);

    void ChangeTarget(Transform t)
    {//registered to OnDeath event of towers enrolled in this soldier
        if (currentTarget == t)
        {
            currentTarget = null;
        }
        MyTargets removingTarget = null;
        foreach (MyTargets targets in entityLL)
        {
            if (targets.GetTransfrom() == t)
            {
                removingTarget = targets;
                break;
            }
        }
        if(removingTarget != null)
        {
            entityLL.Remove(removingTarget);
        }
        else
        {
            Debug.LogError("Removing Defense Entity not found");
        }
        if (entityLL.Count > 0)
        {
            bool isFound = false;
            for (LinkedListNode<MyTargets> it = entityLL.First; it != null && !isFound; it = it.Next)
            {
                Transform defenseTransform = it.Value.GetTransfrom();
                if (defenseTransform.gameObject.tag == "Tower")
                {
                    if (CheckCondition(defenseTransform, defenseTransform.GetComponent<Tower>().GetAttackingEntitiesCount()))
                    {
                        currentTarget = defenseTransform;
                        currentState = States.SET;
                        currentTarget.gameObject.GetComponent<Tower>().AddToAttackingEntity(this.transform);//add this soldier to dict. of currenttarget tower
                        agent.SetDestination(currentTarget.position + Vector3.up * 0.4f);
                        isFound = true;
                    }
                }
                else if (defenseTransform.gameObject.tag == "BlockBarricade")
                {
                    if (CheckCondition(defenseTransform, defenseTransform.GetComponent<BlockBarricade>().GetAttackingEntitiesCount()))
                    {
                        currentTarget = defenseTransform;
                        currentState = States.SET;
                        Debug.Log("current target in the soldier script checkcondition" + currentTarget.tag);
                        currentTarget.gameObject.GetComponent<BlockBarricade>().AddToAttackingSoldiers(this.transform);
                        agent.SetDestination(currentTarget.position + Vector3.up * 0.4f);
                        isFound = true;
                    }
                }
            }
        }
        else
        {
            currentState = States.WALK;
            agent.SetDestination(gateEnd.position + Vector3.up * 0.4f);
        }
    }

}
