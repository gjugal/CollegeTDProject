using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TowerController))]
public abstract class Tower : Entity
{

    //Add the states for tower

    protected TowerController towerController;
    protected float initialForce;
    protected Transform currentTarget;
    protected float timeBetweenShoot;
    protected float lastShootTime;
    public Dictionary<string, int> attackingSoldiers = new Dictionary<string, int>();//This is to keep track of type and count of attackingsoldiers for soldier AI


    protected override void Start()
    {
        base.Start();
        myFirstName = "Tower";
    }

    protected void OnEntry(Collider col)//called from OnTriggerEnter of childTowers
    {
            Entity entity = col.gameObject.GetComponent<Entity>();
            entity.OnDeath += RemoveEntity;
            entityLL.AddLast(new MyTargets(col.gameObject.transform, false, entity.myFirstName));
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
        Debug.Log("remove soldier :" + entity);
        entityLL.Remove(FindFromTargets(entity));
        if (entityLL.Count > 0)
        {
            if (currentTarget == entity)
            {
                ChangeTarget();
            }     
        }
        else
        {
            Debug.LogError("Removing entity not found");
        }
    }

    void OnDestroy()
    {
        GameObject.Destroy(gameObject);
    }

    public abstract void ChangeTarget();

    public void AddToAttackingEntity(Transform t)
    {
        MyTargets myTarget = FindFromTargets(t);
        if(myTarget != null)
        { 
            myTarget.SetAttackingMode(true);
        }
        else
        {
            entityLL.AddLast(new MyTargets(t, true, t.gameObject.GetComponent<Entity>().myFirstName));
        }

    }

    void RemoveFromAttackingEntity(Transform t)
    {
        MyTargets myTarget = FindFromTargets(t);
        if (myTarget != null)
        {
            myTarget.SetAttackingMode(false);
        }
        else
        {
            Debug.LogError("Entity not found. Cannot set its attacking mode to false");
        }
    }

    public int[] GetAttackingEntitiesCount()
    {
        int[] entities = new int[Constants.SOLDIER_TYPES];
        foreach(MyTargets t in entityLL)
        {
            string type = t.GetTargetType();
            if(type == "Sword_Soldier")
            {
                entities[Constants.SWORD_SOLDIER]++;
            }else if(type == "Arrow_Soldier")
            {
                entities[Constants.ARROW_SOLDIER]++;
            }else if(type == "Hammer_Soldier")
            {
                entities[Constants.HAMMER_SOLDIER]++;
            }
        }
        return entities;
    }

  
}



