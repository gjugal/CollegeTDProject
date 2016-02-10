using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TowerController))]
public class Tower : Entity {
    TowerController towerController;
   // Queue<Transform> soldierQueue;
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
        //soldierQueue = new Queue<Transform>();
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
            
            soldierEntity.OnDeath += SetTarget;
            /*
            if (soldierQueue.Count == 0)
            {
                currentTarget = col.gameObject.transform;
            }
            else
            {
                soldierQueue.Enqueue(col.gameObject.transform);
            }
            */
            soldierLL.AddLast(col.gameObject.transform);
            if(currentTarget == null)
            {
                LinkedListNode<Transform> firstSoldier = soldierLL.First;
                currentTarget = firstSoldier.Value;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        Transform exitingTransform = col.gameObject.transform;
        LinkedListNode<Transform> exitingEntity = soldierLL.Find(col.gameObject.transform);
        soldierLL.Remove(exitingEntity);
        if(currentTarget == exitingTransform)
        {
            SetTarget();
        }
    }

   
    void SetTarget()
    {
        //if (soldierQueue.Count > 0)
        //{
        //    currentTarget = soldierQueue.Dequeue();
        //}
        
        soldierLL.RemoveFirst();
        if(soldierLL.Count > 0)
        { 
            LinkedListNode<Transform> firstSoldier = soldierLL.First;
            currentTarget = firstSoldier.Value;
        }
    }


}
