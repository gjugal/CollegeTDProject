using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TowerController))]
public abstract class Tower : Entity {

    //Add the states for tower

    protected TowerController towerController;
    protected float initialForce;
    protected LinkedList<Transform> entityLL;
    protected Transform currentTarget;
    protected float timeBetweenShoot;
    protected float lastShootTime;
    public Dictionary<string, int> attackingSoldiers = new Dictionary<string, int>();//This is to keep track of type and count of attackingsoldiers for soldier AI


    protected override void Start()
    {
        base.Start();
        attackingSoldiers.Add("SwordSoldier(Clone)", 0);
        attackingSoldiers.Add("ArrowSoldier(Clone)", 0);
        attackingSoldiers.Add("HammerSoldier(Clone)", 0);
        attackingSoldiers.Add("Soldier(Clone)", 0);

    }

    protected void OnEntry(Collider col)//called from OnTriggerEnter of childTowers
    {
        if (col.CompareTag("Soldier") || col.CompareTag("King"))
        {
            Entity soldierEntity = col.gameObject.GetComponent<Entity>(); 
            soldierEntity.OnDeath += RemoveEntity;
            entityLL.AddLast(col.gameObject.transform);
            //Debug.Log("Added");
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
        LinkedListNode<Transform> exitingEntity = entityLL.Find(entity);
        try
        {
            //bcoz a soldier may be destroyed by other tower before this method
            entityLL.Remove(exitingEntity);
        }
        catch { }
        if (entityLL.Count > 0) {
            if (currentTarget == entity) {
                ChangeTarget();
            }
        }
        //Debug.Log("Removed");
    }

    void OnDestroy() {
        //Debug.Log("On Destory called");
        GameObject.Destroy(gameObject);
    }

    public abstract void ChangeTarget();

    public void AddToAttackingSoldiers(Transform t) {
        string name = t.gameObject.name;
        //Debug.Log("added " + name);
        attackingSoldiers[name] += 1;
        Entity soldierEntity = t.gameObject.GetComponent<Entity>();
        soldierEntity.OnDeath += RemoveFromDictionary;

    }

    void RemoveFromDictionary(Transform t) {
        string name = t.gameObject.name;
        //check if removed entity is soldier

        if (name != "King") {
            attackingSoldiers[name] -= 1;
        }
    }
}



