using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Soldier : Entity
{
    public ScriptableSoldierProps props;
    Transform gateEnd;
    protected Transform currentTarget = null;

    NavMeshAgent agent;
    protected enum States { WALK, SET, ATTACK };//SET is when soldier finds new target and moves towards it for attacking

    protected States currentState;
    protected GameObject OffenseHeadquaters;

    protected override void Start()
    {
        base.Start();
        myFirstName = "Soldier";
        entityLL = new LinkedList<MyTargets>();
        gateEnd = GameObject.Find("Gate").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(gateEnd.position + Vector3.up * 0.4f);
        currentState = States.WALK;
        OffenseHeadquaters = GameObject.Find("OffenseHeadquaters");
    }



    public void TargetEntry(Transform t)//called from censor whenever it detects a towerbase or barricade
    {
        //Debug.Log("Defense details of " + t + " is" + OffenseHeadquaters.GetComponent<OffenseHeadquaters>().Defensedetails[t][0] + OffenseHeadquaters.GetComponent<OffenseHeadquaters>().Defensedetails[t][1] + OffenseHeadquaters.GetComponent<OffenseHeadquaters>().Defensedetails[t][2]);
        if (CheckCondition(t, OffenseHeadquaters.GetComponent<OffenseHeadquaters>().Defensedetails[t]))
        {
            Entity defenseEntity = t.gameObject.GetComponent<Entity>();
            defenseEntity.OnDeath += ChangeTarget;
            entityLL.AddLast(new MyTargets(t, false, defenseEntity.myFirstName));
            //Debug.Log("Soldier's next target added : " + defenseEntity.myFirstName);
            if (currentTarget == null)
            {
                agent.SetDestination(t.position + Vector3.up * 0.4f);
                currentState = States.SET;
                currentTarget = entityLL.First.Value.GetTransfrom();

                //Debug.Log("Target entry" + t.gameObject.tag);
                OffenseHeadquaters.GetComponent<OffenseHeadquaters>().AddMeToDefense(currentTarget, myFirstName);
            }
        }
    }

    protected abstract bool CheckCondition(Transform t, int[] entities_count);

   

    void ChangeTarget(Transform t)//registered to OnDeath event of towers enrolled in this soldier
    {
        try
        {
            if (currentTarget == t)
            {
                OffenseHeadquaters.GetComponent<OffenseHeadquaters>().RemoveMeFromDefense(t, myFirstName);
                agent.SetDestination(gateEnd.position + Vector3.up * 0.4f);
                currentTarget = null;
            }
            entityLL.Remove(FindFromTargets(t));
            if (entityLL.Count > 0)
            {
                currentTarget = entityLL.First.Value.GetTransfrom();
                currentState = States.SET;
                agent.SetDestination(currentTarget.position + Vector3.up * 0.4f);
                OffenseHeadquaters.GetComponent<OffenseHeadquaters>().AddMeToDefense(currentTarget, myFirstName);
            }
            else
            {
                currentState = States.WALK;
                agent.SetDestination(gateEnd.position + Vector3.up * 0.4f);
            }
        }
        catch {
            Debug.Log("Exception catched");
        }
    }

    void OnDestroy()
    {
        if (currentTarget != null) {
            OffenseHeadquaters.GetComponent<OffenseHeadquaters>().RemoveMeFromDefense(currentTarget, myFirstName);
        }
    }
    

}
