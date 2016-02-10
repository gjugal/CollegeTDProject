using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TowerController))]
public class Tower : Entity {

    //Add the states for tower

    TowerController towerController;
    LinkedList<Transform> soldierLL;
    Transform currentTarget;
    public float timeBetweenShoot = 0.5f;
    float lastShootTime = 0;
    public float towerHealth;


    protected override void Start()
    {
        base.Start();
        health = towerHealth;
        towerController = GetComponent<TowerController>();
        soldierLL = new LinkedList<Transform>();
    }

    void Update()
    {
        if(currentTarget != null)
        {
            towerController.LookAtEnemy(currentTarget);
            if(Time.time > lastShootTime + timeBetweenShoot)
            {
                lastShootTime = Time.time;
                towerController.Shoot();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Soldier") || col.CompareTag("King"))
        {
            Entity soldierEntity = col.gameObject.GetComponent<Entity>(); 
            soldierEntity.OnDeath += RemoveEntity;
            soldierLL.AddLast(col.gameObject.transform);
            if(currentTarget == null)
            {
                LinkedListNode<Transform> next = soldierLL.First;
                SetTarget(next.Value);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        Transform exitingTransform = col.gameObject.transform;
        RemoveEntity(exitingTransform);
    }

   
    void SetTarget(Transform target)
    {
            currentTarget = target;
    }

    void RemoveEntity(Transform entity)
    {
        LinkedListNode<Transform> exitingEntity = soldierLL.Find(entity);
        soldierLL.Remove(exitingEntity);
        if (soldierLL.Count > 0)
        {
            if (currentTarget == entity)
            {
                LinkedListNode<Transform> next = soldierLL.First;
                SetTarget(next.Value);
            }
        }
        else
        {
            currentTarget = null;
            //Change the currentState of tower to idle  
        }
    }

}



