using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ArrowSoldier : Soldier
{
    public float soldier_health = 5f;


    Color originalColor;
    float timeBetweenShoots = 1;
    float lastShootTime = 0;
    float damage = 1;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        myFirstName = "Arrow_Soldier";
        health = soldier_health;

        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        if (currentState == States.ATTACK && currentTarget != null)
        {
            if (Time.time > lastShootTime + timeBetweenShoots)
            {
                lastShootTime = Time.time;
                Shoot();
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = originalColor;
            }
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
        currentTarget.gameObject.GetComponent<Entity>().TakeDamage(damage);
    }
}
