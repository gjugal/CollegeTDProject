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
        Debug.Log(health);
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
        base.OnEntry(col);//Add to list and register tower ondeath
        base.SetTarget(FindTarget());
    }

    void OnTriggerExit(Collider col)
    {
        base.OnExit(col);//Remove from list And ChangeTarget() is called from removeentity()
        base.SetTarget(FindTarget());
    }

   public Transform FindTarget() {//returns target according to AI
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

    public override void ChangeTarget() {//find new target and set it to cuurent // called from remove from tower.entity()
        base.SetTarget(FindTarget());
    }


}
