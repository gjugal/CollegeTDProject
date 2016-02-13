using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowTower : Tower {

    public float initialForceC;
    public float timeBetweenShootc = 0.5f;
    float lastShootTimec = 0;
    public float towerHealth;
    public enum levels {Level1, Level2,Level3, Level4 };
    public levels level = levels.Level1;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        initialForce = initialForceC;
        timeBetweenShoot = timeBetweenShootc;
        lastShootTime = lastShootTimec;
        health = towerHealth;
        towerController = GetComponent<TowerController>();
        entityLL = new LinkedList<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(health);
        if (currentTarget != null)
        {
            towerController.LookAtEnemy(currentTarget);
            if (Time.time > lastShootTime + timeBetweenShoot)
            {
                lastShootTime = Time.time;
                towerController.Shoot(initialForce);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //base.OnEntry(col);
        if (col.CompareTag("Soldier") || col.CompareTag("King"))
        {
            Entity soldierEntity = col.gameObject.GetComponent<Entity>();
            soldierEntity.OnDeath += ChangeTarget;
            entityLL.AddLast(col.gameObject.transform);
            //Debug.Log("Added");
        }
        base.SetTarget(FindTarget());
    }

    void OnTriggerExit(Collider col)
    {
        base.OnExit(col);
        base.SetTarget(FindTarget());
    }

   public Transform FindTarget() {
        if (entityLL.Count == 1) {
            return entityLL.First.Value;
        }
        switch (level) {
            case levels.Level1:
                //AI FOR LEVEL1 -- first come first serve
                if (entityLL.Count > 0)
                {
                    return  entityLL.First.Value;
                }
                break;

            default:
                return null;
        }
        return null;
    }

    public void ChangeTarget(Transform t) {
        base.RemoveEntity(t);
        base.SetTarget(FindTarget());
    }


}
