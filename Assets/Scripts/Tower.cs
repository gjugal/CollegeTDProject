using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TowerController))]
public abstract class Tower : Entity
{

    //Add the states for tower

    protected TowerController towerController;
    protected float initialForce;
    protected LinkedList<MyTargets> entityLL;
    protected Transform currentTarget;
    protected float timeBetweenShoot;
    protected float lastShootTime;
    public Dictionary<string, int> attackingSoldiers = new Dictionary<string, int>();//This is to keep track of type and count of attackingsoldiers for soldier AI


    protected override void Start()
    {
        base.Start();
        //attackingSoldiers.Add("SwordSoldier(Clone)", 0);
        //attackingSoldiers.Add("ArrowSoldier(Clone)", 0);
        //attackingSoldiers.Add("HammerSoldier(Clone)", 0);

    }

    protected void OnEntry(Collider col)//called from OnTriggerEnter of childTowers
    {
        if (col.CompareTag("Soldier") || col.CompareTag("King"))
        {
            Entity soldierEntity = col.gameObject.GetComponent<Entity>();
            soldierEntity.OnDeath += RemoveEntity;

            entityLL.AddLast(new MyTargets(col.gameObject.transform, false));
        }
    }

    protected void OnExit(Collider col)
    {
        Transform exitingTransform = col.gameObject.transform;
        RemoveEntity(exitingTransform);
    }


    protected void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    protected void RemoveEntity(Transform entity)
    {
        MyTargets removingTarget = null;
        foreach(MyTargets targets in entityLL)
        { 
            if (targets.GetTransfrom() == entity)
            {
                removingTarget = targets;
                break;
            }
            else
            {
                Debug.LogError("Removing entity nit found");
            }
        }
        if (removingTarget != null)
        {
            LinkedListNode<MyTargets> exitingEntity = entityLL.Find(removingTarget);
            entityLL.Remove(exitingEntity);
            if (entityLL.Count > 0)
            {
                if (currentTarget == entity)
                {
                    ChangeTarget();
                }
            }
        }
    }

    void OnDestroy()
    {
        GameObject.Destroy(gameObject);
    }

    public abstract void ChangeTarget();

    public void AddToAttackingEntity(Transform t)
    {
        MyTargets myTarget = null;
        foreach(MyTargets target in entityLL)
        {
            if(target.GetTransfrom() == t)
            {
                myTarget = target;
                break;
            }
        }
        if(myTarget != null)
        {
            myTarget.SetAttackingMode(true);
        }
        else
        {
            entityLL.AddLast(new MyTargets(t, true));
        }

    }

    void RemoveFromAttackingEntity(Transform t)
    {
        MyTargets myTarget = null;
        foreach (MyTargets target in entityLL)
        {
            if (target.GetTransfrom() == t)
            {
                myTarget = target;
                break;
            }
        }
        if (myTarget != null)
        {
            myTarget.SetAttackingMode(false);
        }
        else
        {
            Debug.LogError("Entity not found. Cannot set its attacking mode to false");
        }
    }

    protected class MyTargets
    {
        Transform myTransform;
        bool attackState;

        public MyTargets(Transform _tranform, bool _state)
        {
            myTransform = _tranform;
            attackState = _state;
        }

        public void SetAttackingMode(bool state)
        {
            attackState = state;
        }

        public bool GetAttackingMode()
        {
            return attackState;
        }

        public Transform GetTransfrom()
        {
            return myTransform;
        }
    }
}



