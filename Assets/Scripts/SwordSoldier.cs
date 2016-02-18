using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordSoldier : Soldier {

    public float soldier_health = 5f;


    Color originalColor;
    float timeBetweenShoots = 1;
    float lastShootTime = 0;
    float damage = 1;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        health = soldier_health;

        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        Debug.Log(currentTarget);
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

    protected override bool CheckCondition(Transform t, Dictionary<string, int> d)
    {// checks condition to add tower in list
        if (d[this.name] < 5)
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
    }

    void Shoot()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        currentTarget.gameObject.GetComponent<Entity>().TakeDamage(damage);
    }
}
