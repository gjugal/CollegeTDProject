using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombTower : Tower
{

    public float initialForceC;
    public float timeBetweenShootc = 0.5f;
    float lastShootTimec = 0;
    public float towerHealth;
    public enum levels { Level1, Level2, Level3, Level4 };
    public levels level;

    // Use this for initialization
    protected override void Start()
    {
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
        //Debug.Log("tower health= "+ health);
        if (currentTarget != null)
        {
            towerController.LookAtEnemy(currentTarget);
            if (Time.time > lastShootTime + timeBetweenShoot)
            {
                lastShootTime = Time.time;
                towerController.Shoot(initialForce);
            }
        }

        if (level == levels.Level2) {
            base.SetTarget(FindTarget());
        }
        //Debug.Log(entityLL.Count);
    }

    void OnTriggerEnter(Collider col)
    {
        //base.OnEntry(col);
        base.OnEntry(col);//Add to list and register tower ondeath
        base.SetTarget(FindTarget());
    }

    void OnTriggerExit(Collider col)
    {
        base.OnExit(col);//Remove from list And ChangeTarget() is called from removeentity()
        base.SetTarget(FindTarget());
    }

    public Transform FindTarget() {//returns target according to AI
        if (entityLL.Count < 1) {
            return null;
        }

        if (entityLL.Count == 1) {
            return entityLL.First.Value;
        }

        switch (level) {

            case levels.Level1:
                //AI FOR LEVEL1 -- first come first serve
                //Debug.Log("this is case level 1");
                return entityLL.First.Value;

            case levels.Level2:
                //AI FOR LEVEL2 -- soldier in center
                //LinkedList<Transform> temp = entityLL;
                //Debug.Log("this is case level 2");
                Vector3 avg = new Vector3(0, 0, 0);
                foreach (Transform t in entityLL) {
                    avg += t.position;
                }
                avg = avg / entityLL.Count;
                //Debug.Log(avg);
                float distance = 100f;
                float tempDistance = 0f;
                Transform temp = null;
                foreach (Transform t in entityLL) {
                    tempDistance = Mathf.Sqrt((avg.x * t.position.x) + (avg.z * t.position.z));
                    if (tempDistance < distance) {
                        distance = tempDistance;
                        temp = t;
                    }
                }

                //temp.position = avg;
                //temp.rotation = Quaternion.identity;
                //temp.localScale = new Vector3(1,1,1);
                //Debug.Log(distance);
                return temp;

            default:
                Debug.Log("this is case default");
                return null;
        }
    }

    public override void ChangeTarget()// finds and set it to current
    {
        base.SetTarget(FindTarget());
    }
}
