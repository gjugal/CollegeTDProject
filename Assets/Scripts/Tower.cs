using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TowerController))]
public class Tower : Entity {

    //Add the states for tower

    protected TowerController towerController;
    protected float initialForce;
    protected LinkedList<Transform> entityLL;
    protected Transform currentTarget;
    protected float timeBetweenShoot;
    protected float lastShootTime;


    protected override void Start()
    {
        base.Start();
    }

    protected void OnEntry(Collider col)
    {
        //if (col.CompareTag("Soldier") || col.CompareTag("King"))
        //{
        //    Entity soldierEntity = col.gameObject.GetComponent<Entity>(); 
        //    soldierEntity.OnDeath += RemoveEntity;
        //    entityLL.AddLast(col.gameObject.transform);
        //    Debug.Log("Added");
        //}
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
        //Debug.Log("Removed");
    }

    void OnDestroy() {
        //Debug.Log("On Destory called");
        GameObject.Destroy(gameObject);
    }
}



