using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HammerSoldier : Soldier
{     

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        SetMyProperties();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        if (currentState == States.ATTACK && currentTarget != null)
        {
            if (Time.time > lastShootTime + timeBetweenShoots)
            {
                lastShootTime = Time.time;
                Shoot();
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material = originalMaterial;
            }
        }
    }

    protected override bool CheckCondition(Transform t, int[] d)
    {// checks condition to add tower in list
        if (d[Constants.HAMMER_SOLDIER] < 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "TowerBase")
        {
            if (currentState == States.SET && col.transform.parent.transform == currentTarget)
            {
                //agent.enabled = false;
                currentState = States.ATTACK;
            }
        }
        else if (col.gameObject.tag == "BlockBarricade")
        {
            if (currentState == States.SET && col.transform == currentTarget)
            {
                //agent.enabled = false;
                currentState = States.ATTACK;
            }
        }
        else if (col.gameObject.tag == "GroundBarricade")
        {
            if (currentState == States.SET && col.transform == currentTarget)
            {
                //agent.enabled = false;
                currentState = States.ATTACK;
            }
        }
    }

    void Shoot()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        Entity entity = currentTarget.gameObject.GetComponent<Entity>();
        int type_of_target = GameManager.GM.GetDefenseType(entity.myFirstName);
        Debug.Log("" + type_of_target);
        entity.TakeDamage(damage * damagePercentage[type_of_target]/100);
    }

    protected override void SetMyProperties()
    {
        int level = StatisticsManager.SM.GetDetails("Hammer_Soldier_State");
        int type = Constants.HAMMER_SOLDIER;
        if (level >= 1)
        {
            if (level > 3)
                level = 1;
            myProperties = StatisticsManager.SM.GetSoldierProperties(type, level);
            myFirstName = myProperties.myFirstname;
            health = myProperties.health;
            originalMaterial = myProperties.originalMaterial;
            timeBetweenShoots = myProperties.timeBetweenShoots;
            damage = myProperties.damage;
            damagePercentage = myProperties.damagePercentage;
        }
        else
        {
            Debug.LogError("You have still not bought the Arrow Soldier");
        }
    }
}
