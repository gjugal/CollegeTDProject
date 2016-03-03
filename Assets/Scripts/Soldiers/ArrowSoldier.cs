using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ArrowSoldier : Soldier
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

        if(currentState == States.SET && this.transform.position == agent.destination + Vector3.up * 0.4f  && currentTarget != null)
        {
            currentState = States.ATTACK;
        }
    }

    protected override bool CheckCondition(Transform t, int[] d)
    {// checks condition to add tower in list
        if (d[Constants.ARROW_SOLDIER] < 5)
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
        base.OnSoldierColliderEntry(col);
    }

    void Shoot()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        Entity entity = currentTarget.gameObject.GetComponent<Entity>();
        int type_of_target = GameManager.GM.GetDefenseType(entity.myFirstName);
        entity.TakeDamage(damage * damagePercentage[type_of_target]/100);
    }

    protected override void SetMyProperties()
    {
        int level = StatisticsManager.SM.GetDetails("Arrow_Soldier_State");
        int type = Constants.ARROW_SOLDIER;
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
            target_priority = myProperties.priority;
        }
        else
        {
            Debug.LogError("You have still not bought the Arrow Soldier");
        }
    }
}
