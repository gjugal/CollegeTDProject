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
        myFirstName = "Bomb_Tower";
        initialForce = initialForceC;
        timeBetweenShoot = timeBetweenShootc;
        lastShootTime = lastShootTimec;
        health = towerHealth;
        towerController = GetComponent<TowerController>();
        entityLL = new LinkedList<MyTargets>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Soldier") || col.CompareTag("King"))
        {
            base.OnEntry(col);//Add to list and register tower ondeath
            base.SetTarget(FindTarget());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Soldier") || col.CompareTag("King"))
        {
            base.OnExit(col);//Remove from list And ChangeTarget() is called from removeentity()
            base.SetTarget(FindTarget());
        }
    }

    public Transform FindTarget() {//returns target according to AI
        if (entityLL.Count < 1) {
            return null;
        }

        if (entityLL.Count == 1) {
            return entityLL.First.Value.GetTransfrom();
        }

        switch (level) {

            case levels.Level1:
                //AI FOR LEVEL1 -- first come first serve
                //Debug.Log("this is case level 1");
                return entityLL.First.Value.GetTransfrom();

            case levels.Level2:
                //AI FOR LEVEL2 -- soldier in center
                //LinkedList<Transform> temp = entityLL;
                //Debug.Log("this is case level 2");
                Vector3 avg = new Vector3(0, 0, 0);
                foreach (MyTargets t in entityLL) {
                    avg += t.GetTransfrom().position;
                }
                avg = avg / entityLL.Count;
                //Debug.Log(avg);
                float distance = 100f;
                float tempDistance = 0f;
                Transform temp = null;
                foreach (MyTargets t in entityLL) {
                    tempDistance = Mathf.Sqrt((avg.x * t.GetTransfrom().position.x) + (avg.z * t.GetTransfrom().position.z));
                    if (tempDistance < distance) {
                        distance = tempDistance;
                        temp = t.GetTransfrom();
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
